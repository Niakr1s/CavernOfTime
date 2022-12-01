namespace CavernOfTime
{
    [ConsoleFormat('F', ConsoleColor.Cyan)]
    public class Fountain : CavernItem
    {
        public override bool InteractWithPlayer(Cavern cavern, out string? logMsg)
        {
            cavern.Player.FountainVisited = true;
            IsActive = false;
            logMsg = $"Player drank from fountain";

            return true;
        }
    }
}