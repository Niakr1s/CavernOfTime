using CavernOfTime.Common;

namespace CavernOfTime
{
    public class Cavern
    {
        public Cavern(CavernMap map)
        {
            Map = map;
            map.PlayerPositionChanged += Map_PlayerPositionChanged;
        }

        public CavernMap Map { get; }



        public Player Player { get; } = new Player();



        public LimitQueue<string> LogHistory { get; } = new LimitQueue<string>(5);

        private void AddToLog(string message)
        {
            LogHistory.Enqueue(message);
        }



        #region Player actions

        /// <summary>
        /// Moves Player. Returns true, if player was actually moved, otherwise false.
        /// </summary>
        /// <param name="direction"></param>
        /// <returns>True, if move was successfull.</returns>
        public bool MovePlayerToDirection(Direction direction)
        {
            Position newPlayerPosition = Map.PlayerPosition.MoveToDirection(direction);
            return MovePlayerToPosition(newPlayerPosition);
        }

        /// <summary>
        /// Almost same as property PlayerPosition setter, but with return statement.
        /// </summary>
        /// <param name="position"></param>
        public bool MovePlayerToPosition(Position position)
        {
            Position oldPosition = Map.PlayerPosition;
            Map.PlayerPosition = position;
            return oldPosition != Map.PlayerPosition;
        }

        /// <summary>
        /// Interacts player with item on Player's position.
        /// </summary>
        /// <returns></returns>
        public bool InteractPlayerWithItem(out CavernItem? item)
        {
            item = Map.GetCavernItem();
            if (item == null) { return false; }

            bool interacted = item.InteractWithPlayer(this, out string? logMsg);
            if (logMsg != null) AddToLog(logMsg);

            if (!item.IsActive)
            {
                Map.RemoveCavernItem();
            }

            return interacted;
        }

        /// <summary>
        /// Interacts player with item on Player's position.
        /// </summary>
        /// <returns></returns>
        public bool PlayerAttackDirection(Direction attackDirection)
        {
            Position attackPosition = Map.PlayerPosition.MoveToDirection(attackDirection);

            bool inBounds = attackPosition.IsInBounds(Map);
            if (!inBounds) { return false; }

            CavernItem? target = Map.GetCavernItem(attackPosition);
            if (target == null) { return false; }

            bool attackSuccess = target.ReceiveAttackFromPlayer(Player.Weapon, out string? logMsg);
            if (logMsg != null) AddToLog(logMsg);

            Map.CleanInactiveCavernItems();

            return attackSuccess;
        }

        #endregion



        #region Event handlers

        private void Map_PlayerPositionChanged(Position prevPosition, Position newPosition)
        {
            AddToLog($"Player moved from {prevPosition} to {newPosition}");
            InteractPlayerWithItem(out CavernItem? _);
        }

        #endregion
    }
}
