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

            bool playerAtExit = cavern.Map.GetCavernItem(cavern.Map.PlayerPosition) is Exit;
            bool fountainVisited = cavern.Player.FountainVisited;
            bool playerNotDead = !cavern.Player.IsDead;

            return playerAtExit && fountainVisited && playerNotDead;
        }
    }
}
