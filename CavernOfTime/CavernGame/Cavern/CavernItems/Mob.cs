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

        public override bool InteractWithPlayer(Cavern cavern, out string? logMsg)
        {
            cavern.Player.Health.TakeDamage(Weapon.Damage);
            logMsg = $"Player took {Weapon.Damage} damage";
            return true;
        }

        public override bool ReceiveAttackFromPlayer(Weapon weapon, out string? logMsg)
        {
            Health.TakeDamage(weapon.Damage);
            logMsg = $"{this.GetType().Name} took {weapon.Damage} damage and has {Health.Value} left";
            return true;
        }

        #endregion
    }
}