namespace CavernOfTime
{
    public class Pit : CavernItem
    {
        public override bool InteractWithPlayer(Cavern cavern, out string? logMsg)
        {
            cavern.Player.IsDead = true;
            logMsg = $"Player fell to pit and died";

            return true;
        }
    }
}