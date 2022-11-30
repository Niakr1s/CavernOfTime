using CavernOfTime.ConsoleCavernGame;

namespace CavernOfTime.ConsoleGame
{
    internal class ConsoleCavernDisplayer : CavernDisplayer
    {
        private const char _PlayerIcon = 'P';

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
                    WriteIcon(cavern, currentPosition);
                }
                Console.WriteLine("|");
            }
            Console.WriteLine(rowStr);
        }

        private void WriteIcon(Cavern cavern, Position position)
        {
            bool playerAtPosition = cavern.PlayerPosition == position;
            if (playerAtPosition)
            {
                WritePlayerIcon(cavern.Player);
            }
            else
            {
                WriteCavernItem(cavern.GetCavernItem(position));
            }
        }

        private void WritePlayerIcon(Player player)
        {
            ConsoleColor color = player.IsDead ? ConsoleColor.Red : ConsoleColor.Green;
            Console.ForegroundColor = color;
            Console.Write(_PlayerIcon);

            Console.ResetColor();
        }

        private void WriteCavernItem(CavernItem? item)
        {
            char icon = item == null ? ' ' : item.Icon();
            Console.Write(icon);
        }

        private void DisplayPlayer(Player player)
        {
            string fountainVisitedStatus = player.FountainVisited ? "Power of fountain aquired." : "Power of fountain not aquired.";
            Console.WriteLine(fountainVisitedStatus);

            string deadStatus = player.IsDead ? "Player is dead" : "Player is alive";
            Console.WriteLine(deadStatus);
        }
    }
}