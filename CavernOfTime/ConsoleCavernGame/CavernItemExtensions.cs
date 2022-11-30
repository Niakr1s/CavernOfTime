namespace CavernOfTime.ConsoleCavernGame
{
    internal static class CavernItemExtensions
    {
        internal static char Icon(this CavernItem item)
        {
            return item switch
            {
                Fountain => 'F',
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
                Pit => ConsoleColor.Magenta,
                Teleport => ConsoleColor.Magenta,
                Mob => ConsoleColor.Red,
                _ => throw new ArgumentException($"Unknown CavernItem {item}"),
            };
        }
    }
}
