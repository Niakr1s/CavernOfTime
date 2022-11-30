namespace CavernOfTime
{
    public record CavernItemsCountConfig(int Fountains, int Pits);

    public class Cavern
    {

        #region Constructors
        public Cavern(int rows, int cols, CavernItemsCountConfig? config = null)
        {
            if (rows <= 2 || cols <= 2) throw new ArgumentException("Rows and cols must be > 2");

            if (config == null) config = CalculateConfig(rows, cols);

            Map = new CavernItem?[rows, cols];

            // Fountains should be first.
            for (int i = 0; i < config.Fountains; i++)
            {
                AddCavernItemAtRandomPosition(new Fountain(), numTries: 5);
            }

            for (int i = 0; i < config.Pits; i++)
            {
                AddCavernItemAtRandomPosition(new Pit());
            }
        }

        public Cavern(int dim) : this(dim, dim) { }

        private static CavernItemsCountConfig CalculateConfig(int rows, int cols)
        {
            int avg = (int)Math.Sqrt((double)(rows * cols)) + 1;

            int fountains = 1 + avg / 10;
            int pits = 1 + avg / 3;

            return new CavernItemsCountConfig(fountains, pits);
        }

        #endregion

        private CavernItem?[,] Map { get; }

        public int Rows { get => Map.GetLength(0); }

        public int Cols { get => Map.GetLength(1); }


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


        #region CavernMap handle

        public bool AddCavernItemAtPosition(
            CavernItem item,
            Position position,
            bool canPlaceOnPlayersPosition = false,
            bool canOverridePreviousCavernItem = false
            )
        {
            if (!canPlaceOnPlayersPosition && position == PlayerPosition) { return false; }

            CavernItem? previousCavernItem = GetCavernItem(position);
            if (previousCavernItem == null)
            {
                AddCavernItemAtPositionMust(item, position);
                return true;
            }
            // From here previousCavernItem is not null.

            if (!canOverridePreviousCavernItem) { return false; }

            AddCavernItemAtPositionMust(item, position);
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <param name="canPlaceOnPlayersPosition"></param>
        /// <param name="canOverridePreviousCavernItem"></param>
        /// <param name="numTries">Number of tries.</param>
        /// <returns></returns>
        public bool AddCavernItemAtRandomPosition(
            CavernItem item,
            bool canPlaceOnPlayersPosition = false,
            bool canOverridePreviousCavernItem = false,
            int numTries = 1
            )
        {
            if (numTries < 0) throw new ArgumentException("numTries should be >= 0");

            for (int i = 0; i < numTries; i++)
            {
                Position randomPosition = Position.RandomInBounds(this);
                bool itemAdded = AddCavernItemAtPosition(item, randomPosition, canPlaceOnPlayersPosition, canOverridePreviousCavernItem);

                if (itemAdded) { return true; }
            }

            return false;
        }

        /// <summary>
        /// Small helper, that just puts cavern item at position.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="position"></param>
        private void AddCavernItemAtPositionMust(CavernItem item, Position position)
        {
            Map[position.Row, position.Col] = item;
        }

        /// <summary>
        /// Gets cavern item at position.
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public CavernItem? GetCavernItem(Position position)
        {
            return Map[position.Row, position.Col];
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

            Map[position.Row, position.Col] = null;
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