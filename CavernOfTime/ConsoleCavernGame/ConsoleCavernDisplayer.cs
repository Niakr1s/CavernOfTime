using CavernOfTime.ConsoleCavernGame;

namespace CavernOfTime.ConsoleGame
{
    internal class ConsoleCavernDisplayer
    {
        #region Display

        private const char _playerIcon = 'P';

        public void Display(Cavern cavern)
        {
            Console.Clear();

            DisplayCavernMap(cavern);
            Console.WriteLine();

            DisplayPlayer(cavern.Player);
            Console.WriteLine();

            DisplayLog(cavern.LogHistory);
            Console.WriteLine();
        }


        #region Display helpers

        private void DisplayCavernMap(Cavern cavern)
        {
            Console.WriteLine($"Cavern:");

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

        private void DisplayPlayer(Player player)
        {
            Console.WriteLine($"Player:");

            string fountainVisitedStatus = player.FountainVisited ? "Power of fountain aquired." : "Power of fountain not aquired.";
            Console.WriteLine(fountainVisitedStatus);

            string deadStatus = player.IsDead ? "Player is dead" : "Player is alive";
            Console.WriteLine(deadStatus);
        }

        public void DisplayLog(IEnumerable<string> log)
        {
            foreach (string message in log)
            {
                Console.WriteLine(message);
            }
        }

        #endregion

        #endregion




        #region WriteIcon

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
            WriteIconWithColor(_playerIcon, player.Health.Color());
        }

        private void WriteCavernItem(CavernItem? item)
        {
            if (item == null)
            {
                Console.Write(' ');
                return;
            }

            WriteIconWithColor(item.Icon(), item.Color());
        }

        private void WriteIconWithColor(char icon, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(icon);
            Console.ResetColor();
        }

        #endregion
    }
}