﻿namespace CavernOfTime
{
    public delegate void PlayerPositionChangedHandler(Cavern cavern, Position prevPosition);

    public delegate void EventLogHandler(string logMsg);

    public record CavernItemsCountConfig(int Fountains, int Pits, int Maelstorms, int Goblins);

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

            for (int i = 0; i < config.Maelstorms; i++)
            {
                AddCavernItemAtRandomPosition(new Teleport());
            }

            for (int i = 0; i < config.Goblins; i++)
            {
                AddCavernItemAtRandomPosition(new Goblin());
            }
        }

        public Cavern(int dim) : this(dim, dim) { }

        private static CavernItemsCountConfig CalculateConfig(int rows, int cols)
        {
            int avg = (int)Math.Sqrt((double)(rows * cols)) + 1;

            int fountains = 1 + avg / 10;
            int pits = 1 + avg / 3;
            int maelstorms = 1 + avg / 4;
            int goblins = 1 + avg / 7;

            return new CavernItemsCountConfig(fountains, pits, maelstorms, goblins);
        }

        #endregion

        private CavernItem?[,] Map { get; }

        public int Rows { get => Map.GetLength(0); }

        public int Cols { get => Map.GetLength(1); }



        #region Events

        public event EventLogHandler? EventLog;

        #endregion



        #region Player related logic

        private Position _playerPosition = new Position(0, 0);


        public event PlayerPositionChangedHandler? PlayerPositionChanged;
        public Position PlayerPosition
        {
            get => _playerPosition;
            set
            {
                bool canMove = value.IsInBounds(this);
                if (!canMove) { return; }

                bool samePosition = _playerPosition == value;
                if (samePosition) { return; }

                _playerPosition = value;
                EventLog?.Invoke($"Player moved to {PlayerPosition}");
                PlayerPositionChanged?.Invoke(this, value);
            }
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
            return MovePlayerToPosition(newPlayerPosition);
        }

        /// <summary>
        /// Almost same as property PlayerPosition setter, but with return statement.
        /// </summary>
        /// <param name="position"></param>
        public bool MovePlayerToPosition(Position position)
        {
            Position oldPosition = PlayerPosition;
            PlayerPosition = position;
            return oldPosition != PlayerPosition;
        }

        /// <summary>
        /// Interacts player with item on Player's position.
        /// </summary>
        /// <returns></returns>
        public bool InteractPlayerWithItem(out CavernItem? item)
        {
            item = GetCavernItem();
            if (item == null) { return false; }

            bool interacted = item.InteractWithPlayer(this, out string? logMsg);
            if (logMsg != null) EventLog?.Invoke(logMsg);

            if (!item.IsActive)
            {
                RemoveCavernItem();
            }

            return interacted;
        }

        /// <summary>
        /// Interacts player with item on Player's position.
        /// </summary>
        /// <returns></returns>
        public bool PlayerAttackDirection(Direction attackDirection)
        {
            Position attackPosition = PlayerPosition.MoveToDirection(attackDirection);

            bool inBounds = attackPosition.IsInBounds(this);
            if (!inBounds) { return false; }

            CavernItem? target = GetCavernItem(attackPosition);
            if (target == null) { return false; }

            bool attackSuccess = target.ReceiveAttackFromPlayer(Player.Weapon, out string? logMsg);
            if (logMsg != null) EventLog?.Invoke(logMsg);

            CleanInactiveCavernItems();

            return attackSuccess;
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

        private void CleanInactiveCavernItems()
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

        #endregion
    }
}