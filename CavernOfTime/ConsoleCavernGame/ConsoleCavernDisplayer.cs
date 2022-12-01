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

            DisplayPlayer(cavern);
            Console.WriteLine();

            DisplayLog(cavern);
            Console.WriteLine();
        }


        #region Display helpers

        private void DisplayCavernMap(Cavern cavern)
        {
            Console.WriteLine($"Cavern:");

            string rowStr = new string('-', cavern.Map.Cols * 2 + 2);

            Console.WriteLine(rowStr);
            for (int row = cavern.Map.Rows - 1; row >= 0; row--)
            {
                for (int col = 0; col < cavern.Map.Cols; col++)
                {
                    Console.Write("|");

                    Position currentPosition = new Position(row, col);
                    WriteIcon(cavern, currentPosition);
                }
                Console.WriteLine("|");
            }
            Console.WriteLine(rowStr);
        }

        private void DisplayPlayer(Cavern cavern)
        {
            string deadStatus = cavern.Player.IsDead ? "dead" : "alive";

            Console.WriteLine($"Player is {deadStatus}.");
            Console.WriteLine($"Health: {cavern.Player.Health} of {cavern.Player.Health.MaxHealth}.");

            string fountainVisitedStatus = cavern.Player.FountainVisited ? "Power of fountain aquired." : "Power of fountain not aquired.";
            Console.WriteLine(fountainVisitedStatus);
        }

        public void DisplayLog(Cavern cavern)
        {
            foreach (string message in cavern.LogHistory)
            {
                Console.WriteLine(message);
            }
        }

        #endregion

        #endregion




        #region WriteIcon

        private void WriteIcon(Cavern cavern, Position position)
        {
            bool playerAtPosition = cavern.Map.PlayerPosition == position;
            if (playerAtPosition)
            {
                WritePlayerIcon(cavern.Player);
            }
            else
            {
                WriteCavernItem(cavern.Map.GetCavernItem(position));
            }
        }

        private void WritePlayerIcon(Player player)
        {
            WriteIconWithColor(_playerIcon, player.Color());
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
            WriteWithColor(icon.ToString(), color);
        }

        private void WriteWithColor(string str, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(str);
            Console.ResetColor();
        }

        #endregion
    }
}