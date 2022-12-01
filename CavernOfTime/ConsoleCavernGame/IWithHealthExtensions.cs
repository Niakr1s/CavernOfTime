namespace CavernOfTime.ConsoleCavernGame
{
    internal static class IWithHealthExtensions
    {
        internal static ConsoleColor Color(this IWithHealth withHealth)
        {
            Health health = withHealth.Health;
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
