namespace CavernOfTime.ConsoleGame
{
    internal class ConsoleUserInteractor : IUserInteractor
    {
        #region Constructors

        public ConsoleUserInteractor()
        {
            KeyboardController = new ConsoleKeyboardController();
            CavernDisplayer = new ConsoleCavernDisplayer();
        }

        #endregion

        #region Members

        private ConsoleKeyboardController KeyboardController { get; }

        private ConsoleCavernDisplayer CavernDisplayer { get; }

        #endregion

        #region Display.

        public void DisplayWelcomScreen()
        {
            Console.WriteLine("Hello! Welcome to Cavern of time game!");
            Console.WriteLine("In this game you'll have to find fountain and come back alive!");
            Console.WriteLine("Good luck!");

            Console.WriteLine();
            Console.WriteLine($"\nKeybindings:\n{KeyboardController.KeybindingsHelp()}\n");
        }

        public void DisplayGoodbyeScreen()
        {
            Console.WriteLine("Thank you for playing!");
        }

        public void DisplayCavern(Cavern cavern)
        {
            CavernDisplayer.Display(cavern);
        }

        public void DisplayError(string errorMessage)
        {
            Console.WriteLine($"Something went wrong, reason: {errorMessage}");
        }

        #endregion




        #region Ask.

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cols"></param>
        /// <param name="rows"></param>
        /// <exception cref=""/>
        public void AskCavernDimensions(out int rows, out int cols)
        {
            Console.Write("Enter wanted cavern rows: ");
            rows = ReadInt();
            Console.Write("Enter wanted cavern cols: ");
            cols = ReadInt();
        }

        public PlayerAction WaitPlayerAction()
        {
            string keys = string.Join(", ", KeyboardController.Keybindings.Keys);

            Console.Write($"Press ({keys}).");
            PlayerAction action = KeyboardController.WaitPlayerAction();

            return action;
        }

        #endregion



        #region Helpers
        private static int ReadInt()
        {
            string input = Console.ReadLine() ?? "";
            return int.Parse(input);
        }

        #endregion
    }
}
