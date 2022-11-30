namespace CavernOfTime.ConsoleCavernGame
{
    internal static class HealthExtensions
    {
        internal static ConsoleColor Color(this Health health)
        {
            if (health.IsDead)
            {
                return ConsoleColor.Red;
            }
            else if (health.Percent < 20)
            {
                return ConsoleColor.Magenta;
            }
            else if (health.Percent < 60)
            {
                return ConsoleColor.DarkYellow;
            }
            else if (health.Percent < 90)
            {
                return ConsoleColor.DarkGreen;
            }
            else
            {
                return ConsoleColor.Green;
            }
        }
    }
}
