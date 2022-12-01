namespace CavernOfTime
{
    public abstract class Mob : CavernItem
    {
        public Mob(Health health, Weapon weapon)
        {
            Health = health;
            Weapon = weapon;
        }

        public override bool IsActive
        {
            get => base.IsActive && !Health.IsDead;
            set => base.IsActive = value;
        }


        public Health Health { get; }

        public Weapon Weapon { get; }



        #region CavernItem implementations 

        public sealed override bool InteractWithPlayer(Cavern cavern, out string? logMsg)
        {
            return AttackPlayer(cavern.Player, out logMsg);
        }

        public override bool ReceiveAttackFromPlayer(Weapon weapon, out string? logMsg)
        {
            Health.TakeDamage(weapon.Damage);
            logMsg = $"{this.GetType().Name} took {weapon.Damage} damage and has {Health.Value} left";
            return true;
        }

        public virtual bool AttackPlayer(Player player, out string? logMsg)
        {
            player.Health.TakeDamage(Weapon.Damage);
            logMsg = $"Player took {Weapon.Damage} damage from {GetType().Name}";
            return true;
        }

        #endregion
    }
}