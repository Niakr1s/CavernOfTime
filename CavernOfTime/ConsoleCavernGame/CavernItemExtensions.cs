namespace CavernOfTime.ConsoleCavernGame
{
    internal static class CavernItemExtensions
    {
        internal static char Icon(this CavernItem item)
        {
            return item switch
            {
                Fountain => 'F',
                Exit => 'E',
                Pit => 'X',
                Teleport => 'T',
                Mob => 'M',
                _ => throw new ArgumentException($"Unknown CavernItem {item}"),
            };
        }

        internal static ConsoleColor Color(this CavernItem item)
        {
            return item switch
            {
                Fountain => ConsoleColor.Cyan,
                Exit => ConsoleColor.Cyan,
                Pit => ConsoleColor.Magenta,
                Teleport => ConsoleColor.Magenta,
                Mob m => m.Health.Color(),
                _ => throw new ArgumentException($"Unknown CavernItem {item}"),
            };
        }
    }
}
