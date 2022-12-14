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

        private void AddToLog(string? message)
        {
            if (message != null) { LogHistory.Enqueue(message); }
        }



        #region Player actions

        /// <summary>
        /// Moves Player. Returns true, if player was actually moved, otherwise false.
        /// </summary>
        /// <param name="direction"></param>
        /// <returns>True, if move was successfull.</returns>
        public bool MovePlayerToDirection(Direction direction)
        {
            Position newPlayerPosition = Map.PlayerPosition.To(direction);
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

            OnTurnEnd();
            return oldPosition != Map.PlayerPosition;
        }

        /// <summary>
        /// Interacts player with item on Player's position.
        /// </summary>
        /// <returns></returns>
        public bool InteractPlayerWithItem(out CavernItem? item)
        {
            Position itemPosition = Map.PlayerPosition;

            item = Map.GetCavernItem(itemPosition);
            if (item == null) { return false; }

            // We want attack before.
            if (item is Mob)
            {
                item.ReceiveAttackFromPlayer(Player.Weapon, out string? attackLogMsg);
                AddToLog(attackLogMsg);
            }

            bool interacted = item.InteractWithPlayer(this, out string? logMsg);
            AddToLog(logMsg);

            if (!item.IsActive)
            {
                Map.RemoveCavernItem(itemPosition);
            }

            OnTurnEnd();
            return interacted;
        }

        /// <summary>
        /// Interacts player with item on Player's position.
        /// </summary>
        /// <returns></returns>
        public bool PlayerAttack(Direction attackDirection)
        {
            Position attackPosition = Map.PlayerPosition.To(attackDirection);

            bool inBounds = attackPosition.IsInBounds(Map);
            if (!inBounds) { return false; }

            CavernItem? target = Map.GetCavernItem(attackPosition);
            if (target == null) { return false; }

            bool attackSuccess = target.ReceiveAttackFromPlayer(Player.Weapon, out string? attackLogMsg);
            AddToLog(attackLogMsg);

            Map.CleanInactiveCavernItems();

            OnTurnEnd();
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



        #region OnTurnEnd

        private void OnTurnEnd()
        {
            MobsTurn();
        }

        private void MobsTurn()
        {
            foreach (Mob m in Map.GetCavernItems(skipItemAtPlayerPosition: true).OfType<Mob>())
            {
                m.AttackPlayer(this, out string? attackLogMsg);
                AddToLog(attackLogMsg);
            }
        }

        #endregion
    }
}
