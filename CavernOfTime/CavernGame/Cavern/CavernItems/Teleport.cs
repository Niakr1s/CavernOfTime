namespace CavernOfTime
{
    [ConsoleFormat('T', ConsoleColor.Magenta)]
    public class Teleport : CavernItem
    {
        public override bool InteractWithPlayer(Cavern cavern, out string? logMsg)
        {
            Position jumpToPosition = cavern.Map.PlayerPosition;
            while (jumpToPosition == cavern.Map.PlayerPosition)
            {
                jumpToPosition = Position.RandomInBounds(cavern.Map);
            }
            Position oldPosition = cavern.Map.PlayerPosition;
            cavern.Map.PlayerPosition = jumpToPosition;

            logMsg = $"Player teleported from {oldPosition} to {cavern.Map.PlayerPosition};";

            return true;
        }
    }
}