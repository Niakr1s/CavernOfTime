namespace CavernOfTime
{
    public class StandardRules : IRules
    {
        public bool IsLoss(Cavern cavern)
        {
            return false;
        }

        public bool IsWin(Cavern cavern)
        {
            bool playerAtStart = cavern.PlayerPosition == new Position(0, 0);
            bool fountainVisited = cavern.Player.FountainVisited;

            return playerAtStart && fountainVisited;
        }
    }
}
