namespace CavernOfTime
{
    public class Cavern
    {

        #region Constructors
        public Cavern(int rows, int cols)
        {
            if (rows <= 2 || cols <= 2) throw new ArgumentException("Rows and cols must be > 2");
            CavernMap = new CavernItem?[rows, cols];
            CavernMap[0, 2] = new Fountain();
        }

        public Cavern(int dim) : this(dim, dim) { }
        #endregion

        private CavernItem?[,] CavernMap { get; }

        public int Rows { get => CavernMap.GetLength(0); }

        public int Cols { get => CavernMap.GetLength(1); }


        #region Player related logic

        private Position _playerPosition = new Position(0, 0);

        public Position PlayerPosition
        {
            get => _playerPosition;
            set => _playerPosition = value;
        }

        public Player Player { get; } = new Player();

        #endregion


        #region Player interactions

        /// <summary>
        /// Moves Player. Returns true, if player was actually moved, otherwise false.
        /// </summary>
        /// <param name="direction"></param>
        /// <returns>True, if move was successfull.</returns>
        public bool MovePlayerToDirection(Direction direction)
        {
            Position newPlayerPosition = PlayerPosition.MoveToDirection(direction);
            bool canMove = newPlayerPosition.IsInBounds(this);
            if (!canMove) { return false; }

            PlayerPosition = newPlayerPosition;
            return true;
        }

        /// <summary>
        /// Interacts player with item on Player's position.
        /// </summary>
        /// <returns></returns>
        public bool InteractPlayerWithItem(out CavernItem? item)
        {
            item = GetCavernItem();
            if (item == null) { return false; }

            bool interacted = item.Interact(Player);
            if (!item.IsActive)
            {
                RemoveCavernItem();
            }

            return interacted;
        }

        #endregion


        #region Helpers

        /// <summary>
        /// Gets cavern item at position.
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public CavernItem? GetCavernItem(Position position)
        {
            return CavernMap[position.Row, position.Col];
        }

        /// <summary>
        /// Gets cavern item at player's position.
        /// </summary>
        /// <returns></returns>
        public CavernItem? GetCavernItem()
        {
            return GetCavernItem(PlayerPosition);
        }

        /// <summary>
        /// Removes cavern item at position.
        /// </summary>
        /// <param name="position"></param>
        /// <returns>True, if item was actually deleted</returns>
        public bool RemoveCavernItem(Position position)
        {
            CavernItem? item = GetCavernItem(position);
            if (item == null) { return false; }

            CavernMap[position.Row, position.Col] = null;
            return true;
        }

        /// <summary>
        /// Removes cavern item at player's position.
        /// </summary>
        /// <returns>True, if item was actually deleted</returns>
        private bool RemoveCavernItem()
        {
            return RemoveCavernItem(PlayerPosition);
        }

        #endregion
    }
}