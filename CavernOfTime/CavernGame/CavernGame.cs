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
            cavern.EventLog += OnEventLog;

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
                PlayerMoveToDirection(cavern, direction);
                return;
            }

            if (action.ShootDirection is Direction attackDirection)
            {
                PlayerAttackDirection(cavern, attackDirection);
                return;
            }

            if (action.WantInteract)
            {
                PlayerInteractWithItem(cavern);
                return;
            }
        }

        private void PlayerMoveToDirection(Cavern cavern, Direction direction)
        {
            cavern.MovePlayerToDirection(direction);
        }

        private void PlayerAttackDirection(Cavern cavern, Direction attackDirection)
        {
            cavern.PlayerAttackDirection(attackDirection);
        }

        private void PlayerInteractWithItem(Cavern cavern)
        {
            cavern.InteractPlayerWithItem(out CavernItem? _);
        }

        #endregion


        #region Event handlers

        private void OnPlayerPositionChanged(Cavern cavern, Position position)
        {
            PlayerInteractWithItem(cavern);
        }

        private void OnEventLog(string logMsg)
        {
            UserInteractor.AddToLog(logMsg);
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
