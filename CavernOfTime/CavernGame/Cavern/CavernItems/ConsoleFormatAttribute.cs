namespace CavernOfTime
{
    public class ConsoleFormatAttribute : Attribute
    {
        public ConsoleFormatAttribute(char icon, ConsoleColor color) : this(icon)
        {
            Color = color;
        }

        public ConsoleFormatAttribute(char icon)
        {
            Icon = icon;
        }


        public char Icon { get; }

        public ConsoleColor? Color { get; }
    }
}
