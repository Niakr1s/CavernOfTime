namespace CavernOfTime
{
    public class Teleport : CavernItem
    {
        public override bool InteractWithPlayer(Cavern cavern)
        {
            Position jumpToPosition = cavern.PlayerPosition;
            while (jumpToPosition == cavern.PlayerPosition)
            {
                jumpToPosition = Position.RandomInBounds(cavern);
            }
            cavern.PlayerPosition = jumpToPosition;
            return true;
        }
    }
}