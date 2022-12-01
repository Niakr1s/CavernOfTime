using System.Reflection;

namespace CavernOfTime.ConsoleCavernGame
{
    internal static class CavernItemExtensions
    {
        internal static char Icon(this CavernItem item)
        {
            ConsoleFormatAttribute? formatAttr =
                item.GetType().GetCustomAttribute<ConsoleFormatAttribute>();

            if (formatAttr == null)
            {
                throw new ArgumentException($"No format attribute for {item}");
            }

            return formatAttr.Icon;
        }

        internal static ConsoleColor Color(this CavernItem item)
        {
            ConsoleFormatAttribute? formatAttr =
                item.GetType().GetCustomAttribute<ConsoleFormatAttribute>();

            ConsoleColor? attrColor = formatAttr?.Color;

            if (attrColor != null) { return attrColor.Value; }

            if (item is IWithHealth h) { return h.Color(); }

            return ConsoleColor.White;
        }
    }
}
