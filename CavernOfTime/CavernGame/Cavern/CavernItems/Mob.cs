namespace CavernOfTime
{
    public abstract class Mob : CavernItem
    {
        public override bool InteractWithPlayer(Cavern cavern)
        {
            cavern.Player.IsDead = true;
            return true;
        }

        public override bool ReceiveAttackFromPlayer(Weapon weapon)
        {
            IsActive = false;
            return true;
        }
    }
}