namespace CavernOfTime.ConsoleGame
{
    internal class ConsoleCavernDisplayer : CavernDisplayer
    {
        private const char _PlayerIcon = 'P';
        private const char _FountainIcon = 'F';

        public override void Display(Cavern cavern)
        {
            Console.WriteLine($"Cavern:");
            DisplayCavernMap(cavern);

            Console.WriteLine();

            Console.WriteLine($"Player:");
            DisplayPlayer(cavern.Player);

            Console.WriteLine();
        }

        private void DisplayCavernMap(Cavern cavern)
        {
            string rowStr = new string('-', cavern.Cols * 2 + 2);

            Console.WriteLine(rowStr);
            for (int row = cavern.Rows - 1; row >= 0; row--)
            {
                for (int col = 0; col < cavern.Cols; col++)
                {
                    Console.Write("|");

                    Position currentPosition = new Position(row, col);
                    char icon = GetIconForPosition(cavern, currentPosition);

                    Console.Write(icon);
                }
                Console.WriteLine("|");
            }
            Console.WriteLine(rowStr);
        }

        private char GetIconForPosition(Cavern cavern, Position position)
        {
            char displayIcon = ' ';

            bool playerAtPosition = cavern.PlayerPosition == position;
            if (playerAtPosition)
            {
                displayIcon = _PlayerIcon;
            }
            else
            {
                CavernItem? item = cavern.GetCavernItem(position);
                if (item != null)
                {
                    displayIcon = GetIconForCavernItem(item);
                }
            }

            return displayIcon;
        }

        private char GetIconForCavernItem(CavernItem item)
        {
            if (item is Fountain)
            {
                return _FountainIcon;
            }

            throw new ArgumentException($"Unknown CavernItem {item}");
        }

        private void DisplayPlayer(Player player)
        {
            if (player.FountainVisited)
            {
                Console.WriteLine("Power of fountain aquired.");
            }
            else
            {
                Console.WriteLine("Power of fountain not aquired.");
            }
        }
    }
}
