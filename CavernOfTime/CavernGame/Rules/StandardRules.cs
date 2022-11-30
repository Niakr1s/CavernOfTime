namespace CavernOfTime
{
    public class StandardRules : IRules
    {
        public bool IsLoss(Cavern cavern)
        {
            return cavern.Player.IsDead;
        }

        public bool IsWin(Cavern cavern)
        {
            bool playerAtStart = cavern.PlayerPosition == new Position(0, 0);
            bool fountainVisited = cavern.Player.FountainVisited;
            bool playerNotDead = !cavern.Player.IsDead;

            return playerAtStart && fountainVisited && playerNotDead;
        }
    }
}
