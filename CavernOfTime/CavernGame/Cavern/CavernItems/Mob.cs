namespace CavernOfTime
{
    [ConsoleFormat('M')]
    public abstract class Mob : CavernItem, IWithHealth
    {
        public Mob(Health health)
        {
            Health = health;
        }

        public override bool IsActive
        {
            get => base.IsActive && !Health.IsDead;
            set => base.IsActive = value;
        }


        public Health Health { get; }

        public Weapon? MeleeWeapon { get; set; }

        public Weapon? RangedWeapon { get; set; }


        #region CavernItem implementations 

        public sealed override bool InteractWithPlayer(Cavern cavern, out string? logMsg)
        {
            return AttackPlayer(cavern, out logMsg);
        }

        public override bool ReceiveAttackFromPlayer(Weapon weapon, out string? logMsg)
        {
            Health.TakeDamage(weapon.Damage);
            logMsg = $"{this.GetType().Name} took {weapon.Damage} damage and has {Health.Value} left";
            return true;
        }

        public virtual bool AttackPlayer(Cavern cavern, out string? logMsg)
        {
            logMsg = null;
            if (Position == null) return false;


            // Melee
            bool samePositionAsPlayer = Position == cavern.Map.PlayerPosition;
            if (samePositionAsPlayer)
            {
                if (MeleeWeapon == null) { return false; }

                cavern.Player.Health.TakeDamage(MeleeWeapon.Damage);
                logMsg = $"Player took {MeleeWeapon.Damage} melee damage from {GetType().Name}";

                return true;
            }

            if (RangedWeapon == null) return false;


            // Ranged
            InLineRelation? relation = Position.InLineRelation(cavern.Map.PlayerPosition);
            if (relation == null) return false;
            if (!relation.Value.IsReachable(RangedWeapon)) { return false; }

            cavern.Player.Health.TakeDamage(RangedWeapon.Damage);
            logMsg = $"Player took {RangedWeapon.Damage} ranged damage from {GetType().Name}";

            return true;
        }

        #endregion
    }
}