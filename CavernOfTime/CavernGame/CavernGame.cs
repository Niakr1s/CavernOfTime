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

            Cavern cavern = CreateCavern();
            UserInteractor.StepEnd();

            do
            {
                CavernDisplayer.Display(cavern);

                PlayerAction action = KeyboardController.WaitPlayerAction();

                HandlePlayerAction(cavern, action);

                UserInteractor.StepEnd();
            } while (!Rules.GameEnded(cavern));

            UserInteractor.SayGoodbye();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cavern"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        private void HandlePlayerAction(Cavern cavern, PlayerAction action)
        {
            if (action.WantInteract)
            {
                bool interacted = cavern.InteractPlayerWithItem(out CavernItem? interactedItem);
                if (interacted)
                {
                    UserInteractor.Say($"Player interacted with item {interactedItem}!");
                }
                return;
            }

            if (action.Direction is Direction direction)
            {
                bool playerMoved = cavern.MovePlayerToDirection(direction);
                if (playerMoved)
                    UserInteractor.Say($"Player moved to {action}");
                else
                {
                    UserInteractor.SayError($"Player couldn't move to {action}");
                }
                return;
            }
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
