namespace CavernOfTime
{
    public class CavernGame : ICavernGame
    {
        #region Constructors
        public CavernGame(IUserInteractor io, ICavernDisplayer cavernDisplayer, IKeyboardController keyboardController, IRules rules)
        {
            UserInteractor = io;
            CavernDisplayer = cavernDisplayer;
            KeyboardController = keyboardController;
            Rules = rules;
        }
        #endregion

        #region Systems

        private IUserInteractor UserInteractor { get; }

        private IKeyboardController KeyboardController { get; }

        private ICavernDisplayer CavernDisplayer { get; }

        private IRules Rules { get; }

        #endregion

        #region ICavernGame implementation
        public void Start()
        {
            UserInteractor.SayWelcome();
            UserInteractor.Say($"\nKeybindings:\n{KeyboardController.KeybindingsHelp()}\n");

            Cavern cavern = CreateCavern();
            UserInteractor.StepEnd();

            cavern.PlayerPositionChanged += OnPlayerPositionChanged;

            do
            {
                CavernDisplayer.Display(cavern);
                UserInteractor.ShowLog();

                PlayerAction action = KeyboardController.WaitPlayerAction();

                HandlePlayerAction(cavern, action);

                UserInteractor.StepEnd();
            } while (!Rules.GameEnded(cavern));

            cavern.PlayerPositionChanged -= OnPlayerPositionChanged;

            CavernDisplayer.Display(cavern);
            UserInteractor.ShowLog();

            UserInteractor.SayGoodbye();
        }

        #endregion



        #region Player actions

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cavern"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        private void HandlePlayerAction(Cavern cavern, PlayerAction action)
        {
            if (action.Direction is Direction direction)
            {
                MovePlayerToDirection(cavern, direction);
                return;
            }

            if (action.ShootDirection is Direction attackDirection)
            {
                PlayerAttackDirection(cavern, attackDirection);
                return;
            }

            if (action.WantInteract)
            {
                InteractPlayerWithItem(cavern);
                return;
            }
        }

        private void MovePlayerToDirection(Cavern cavern, Direction direction)
        {
            bool moved = cavern.MovePlayerToDirection(direction);
            if (moved)
            {
                string logMessage = $"Player moved to {direction}";
                UserInteractor.AddToLog(logMessage);
            }
        }

        private void PlayerAttackDirection(Cavern cavern, Direction attackDirection)
        {
            bool attacked = cavern.PlayerAttackDirection(attackDirection);
            if (attacked)
            {
                string logMessage = $"Attacked to {attackDirection}";
                UserInteractor.AddToLog(logMessage);
            }
        }

        private void InteractPlayerWithItem(Cavern cavern)
        {
            bool interacted = cavern.InteractPlayerWithItem(out CavernItem? interactedCavernItem);
            if (interacted)
            {
                string logMessage = $"Player interacted with {interactedCavernItem}";
                UserInteractor.AddToLog(logMessage);
            }

        }

        #endregion


        #region Event handlers

        private void OnPlayerPositionChanged(Cavern cavern, Position position)
        {
            InteractPlayerWithItem(cavern);
        }

        #endregion


        #region Helpers
        /// <summary>
        /// Returns valid Cavern. Doesn't throw exceptions.
        /// </summary>
        /// <returns></returns>
        private Cavern CreateCavern()
        {
            Cavern? cavern = null;
            do
            {
                try
                {
                    UserInteractor.AskCavernDimensions(out int rows, out int cols);
                    cavern = new Cavern(rows, cols);
                }
                catch (Exception e)
                {
                    UserInteractor.SayError(e);
                }

            } while (cavern == null);

            return cavern;
        }
        #endregion
    }
}
