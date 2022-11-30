namespace CavernOfTime
{
    public abstract class Mob : CavernItem
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



        #region CavernItem implementations 

        public override bool InteractWithPlayer(Cavern cavern)
        {
            cavern.Player.IsDead = true;
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