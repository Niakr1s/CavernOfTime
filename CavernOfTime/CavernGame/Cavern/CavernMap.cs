namespace CavernOfTime
{
    public delegate void PlayerPositionChangedHandler(Position prevPosition, Position newPosition);

    public record CavernMapItemsCountConfig(int Fountains, int Pits, int Maelstorms, int Goblins);

    public class CavernMap
    {

        #region Constructors
        public CavernMap(int rows, int cols, CavernMapItemsCountConfig? config = null)
        {
            if (rows <= 2 || cols <= 2) throw new ArgumentException("Rows and cols must be > 2");

            if (config == null) config = CalculateConfig(rows, cols);

            Map = new CavernItem?[rows, cols];

            // Exit should be first.
            AddCavernItemAtRandomPosition(new Exit(), numTries: 100);

            for (int i = 0; i < config.Fountains; i++)
            {
                AddCavernItemAtRandomPosition(new Fountain(), numTries: 100);
            }

            for (int i = 0; i < config.Pits; i++)
            {
                AddCavernItemAtRandomPosition(new Pit());
            }

            for (int i = 0; i < config.Maelstorms; i++)
            {
                AddCavernItemAtRandomPosition(new Teleport());
            }

            for (int i = 0; i < config.Goblins; i++)
            {
                AddCavernItemAtRandomPosition(new Goblin());
            }
        }

        public CavernMap(int dim) : this(dim, dim) { }

        private static CavernMapItemsCountConfig CalculateConfig(int rows, int cols)
        {
            int avg = (int)Math.Sqrt((double)(rows * cols)) + 1;

            int fountains = 1 + avg / 10;
            int pits = 1 + avg / 3;
            int maelstorms = 1 + avg / 4;
            int goblins = 1 + avg / 7;

            return new CavernMapItemsCountConfig(fountains, pits, maelstorms, goblins);
        }

        #endregion

        private CavernItem?[,] Map { get; }

        public int Rows { get => Map.GetLength(0); }

        public int Cols { get => Map.GetLength(1); }



        #region Player related logic

        public event PlayerPositionChangedHandler? PlayerPositionChanged;

        private Position _playerPosition = new Position(0, 0);

        public Position PlayerPosition
        {
            get => _playerPosition;
            set
            {
                bool canMove = value.IsInBounds(this);
                if (!canMove) { return; }

                Position prevPosition = _playerPosition;
                bool samePosition = prevPosition == value;
                if (samePosition) { return; }

                _playerPosition = value;
                PlayerPositionChanged?.Invoke(prevPosition, value);
            }
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
            item.Position = position;
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

        public void CleanInactiveCavernItems()
        {
            for (int row = 0; row < Rows; row++)
            {
                for (int col = 0; col < Cols; col++)
                {
                    Position position = new Position(row, col);
                    CavernItem? item = GetCavernItem(new Position(row, col));
                    if (item != null && !item.IsActive) { RemoveCavernItem(position); }
                }
            }
        }

        public List<CavernItem> GetCavernItems(bool skipItemAtPlayerPosition = false)
        {
            var items = new List<CavernItem>();
            for (int row = 0; row < Rows; row++)
            {
                for (int col = 0; col < Cols; col++)
                {
                    Position position = new Position(row, col);
                    if (skipItemAtPlayerPosition && position == PlayerPosition) { continue; }

                    CavernItem? item = GetCavernItem(position);
                    if (item == null) { continue; }

                    items.Add(item);
                }
            }
            return items;
        }

        #endregion
    }
}