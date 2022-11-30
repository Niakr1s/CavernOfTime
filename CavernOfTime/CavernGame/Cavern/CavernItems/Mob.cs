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

        public override bool InteractWithPlayer(Cavern cavern)
        {
            cavern.Player.Health.TakeDamage(Weapon.Damage);
            return true;
        }

        public override bool ReceiveAttackFromPlayer(Weapon weapon)
        {
            Health.TakeDamage(weapon.Damage);
            return true;
        }

        #endregion
    }
}