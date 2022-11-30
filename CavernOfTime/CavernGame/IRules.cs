namespace CavernOfTime
{
    public interface IRules
    {
        bool IsWin(Cavern cavern);

        bool IsLoss(Cavern cavern);

        bool GameEnded(Cavern cavern)
        {
            return IsWin(cavern) || IsLoss(cavern);
        }
    }
}
