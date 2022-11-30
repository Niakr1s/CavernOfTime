namespace CavernOfTime
{
    public class Teleport : CavernItem
    {
        public override bool InteractWithPlayer(Cavern cavern, out string? logMsg)
        {
            Position jumpToPosition = cavern.PlayerPosition;
            while (jumpToPosition == cavern.PlayerPosition)
            {
                jumpToPosition = Position.RandomInBounds(cavern);
            }
            Position oldPosition = cavern.PlayerPosition;
            cavern.PlayerPosition = jumpToPosition;

            logMsg = $"Player teleported from {oldPosition} to {cavern.PlayerPosition};";

            return true;
        }
    }
}