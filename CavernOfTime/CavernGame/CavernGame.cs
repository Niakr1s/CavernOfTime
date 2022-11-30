using CavernOfTime.ConsoleGame;

namespace CavernOfTime
{
    public interface ICavernGame
    {
        void Start();
    }

    public class CavernGame : ICavernGame
    {
        #region Constructors
        private CavernGame(IUserInteractor io, ICavernDisplayer cavernDisplayer, IRules rules)
        {
            Io = io;
            CavernDisplayer = cavernDisplayer;
            Rules = rules;
        }

        public static CavernGame NewConsoleCavernGame()
        {
            return new CavernGame(new ConsoleUserInteractor(ConsolePlayerMoveController.WsadAndArrows()), new ConsoleCavernDisplayer(), new StandardRules());
        }
        #endregion

        #region Systems

        private IUserInteractor Io { get; }

        private ICavernDisplayer CavernDisplayer { get; }

        private IRules Rules { get; }

        #endregion

        #region ICavernGame implementation
        public void Start()
        {
            Io.SayWelcome();

            Cavern cavern = CreateCavern();

            do
            {
                CavernDisplayer.Display(cavern);

                PlayerAction action = Io.WaitPlayerAction();

                HandlePlayerAction(cavern, action);

                Io.StepEnd();
            } while (!Rules.GameEnded(cavern));

            Io.SayGoodbye();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cavern"></param>
        /// <param name="action"></param>
        /// <returns>False if action wasn't handled.</returns>
        private void HandlePlayerAction(Cavern cavern, PlayerAction action)
        {
            if (action.WantInteract)
            {
                bool interacted = cavern.InteractPlayerWithItem(out CavernItem? interactedItem);
                if (interacted)
                {
                    Io.Say($"Player interacted with item {interactedItem}!");
                }
                return;
            }

            if (action.Direction is Direction direction)
            {
                bool playerMoved = cavern.MovePlayerToDirection(direction);
                if (playerMoved)
                    Io.Say($"Player moved to {action}");
                else
                {
                    Io.SayError($"Player couldn't move to {action}");
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
                    Io.AskCavernDimensions(out int rows, out int cols);
                    cavern = new Cavern(rows, cols);
                }
                catch (Exception e)
                {
                    Io.SayError(e);
                }

                Io.StepEnd();
            } while (cavern == null);

            return cavern;
        }
        #endregion
    }
}
