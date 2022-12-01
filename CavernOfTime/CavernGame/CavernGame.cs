using CavernOfTime.PlayerActions;

namespace CavernOfTime
{
    public class CavernGame
    {
        #region Constructors

        public CavernGame(IRules rules, IIO io)
        {
            Io = io;
            Rules = rules;
        }
        #endregion





        #region Members

        private IIO Io { get; }

        private IRules Rules { get; }

        #endregion




        #region Main function

        public void Start()
        {
            Io.DisplayWelcomScreen();

            Cavern cavern = CreateCavern();

            do
            {
                Io.DisplayCavern(cavern);

                PlayerAction action = Io.WaitPlayerAction();
                HandlePlayerAction(cavern, action);
            } while (!Rules.GameEnded(cavern));

            Io.DisplayCavern(cavern);
            Io.DisplayGoodbyeScreen();
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
            switch (action)
            {
                case PlayerMoveAction moveAction:
                    PlayerMoveToDirection(cavern, moveAction.Direction);
                    break;

                case PlayerShootAction attackAction:
                    PlayerAttackDirection(cavern, attackAction.Direction);
                    break;

                case PlayerInteractRequestAction:
                    PlayerInteractWithItem(cavern);
                    break;

                default:
                    Io.DisplayError($"Unknown action: {action}");
                    break;
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



        #region Helpers

        /// <summary>
        /// Returns valid Cavern, using UserInteractor. Doesn't throw exceptions.
        /// </summary>
        /// <returns></returns>
        private Cavern CreateCavern()
        {
            CavernMap? map = null;
            do
            {
                try
                {
                    Io.AskCavernDimensions(out int rows, out int cols);
                    map = new CavernMap(rows, cols);
                }
                catch (Exception e)
                {
                    Io.DisplayError(e);
                }

            } while (map == null);

            Cavern cavern = new Cavern(map);

            return cavern;
        }

        #endregion
    }
}
