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
    }
}
