namespace CavernOfTime
{
    public class Pit : CavernItem
    {
        public override bool InteractWithPlayer(Cavern cavern)
        {
            cavern.Player.IsDead = true;
            return true;
        }
    }
}