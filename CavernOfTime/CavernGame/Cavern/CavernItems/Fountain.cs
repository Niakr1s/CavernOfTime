namespace CavernOfTime
{
    public class Fountain : CavernItem
    {
        public override bool InteractWithPlayer(Cavern cavern)
        {
            cavern.Player.FountainVisited = true;
            IsActive = false;

            return true;
        }
    }
}