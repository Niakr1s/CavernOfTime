namespace CavernOfTime
{
    public abstract class Mob : CavernItem
    {
        public override bool InteractWithPlayer(Cavern cavern)
        {
            cavern.Player.IsDead = true;
            return true;
        }
    }
}