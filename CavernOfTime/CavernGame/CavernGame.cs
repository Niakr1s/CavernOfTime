namespace CavernOfTime
{
    public class CavernGame
    {
        #region Constructors

        public CavernGame(IRules rules, IUserInteractor userInteractor)
        {
            UserInteractor = userInteractor;
            Rules = rules;
        }
        #endregion





        #region Members

        private IUserInteractor UserInteractor { get; }

        private IRules Rules { get; }

        #endregion




        #region Main function

        public void Start()
        {
            UserInteractor.DisplayWelcomScreen();

            Cavern cavern = CreateCavern();

            cavern.PlayerPositionChanged += OnPlayerPositionChanged;

            do
            {
                UserInteractor.DisplayCavern(cavern);

                PlayerAction action = UserInteractor.WaitPlayerAction();
                HandlePlayerAction(cavern, action);
            } while (!Rules.GameEnded(cavern));

            cavern.PlayerPositionChanged -= OnPlayerPositionChanged;

            UserInteractor.DisplayCavern(cavern);
            UserInteractor.DisplayGoodbyeScreen();
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

        #endregion




        #region Helpers

        /// <summary>
        /// Returns valid Cavern, using UserInteractor. Doesn't throw exceptions.
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
                    UserInteractor.DisplayError(e);
                }

            } while (cavern == null);

            return cavern;
        }

        #endregion
    }
}
