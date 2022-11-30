namespace CavernOfTime.ConsoleGame
{
    internal class ConsoleUserInteractor : IUserInteractor
    {
        #region Constructors

        public ConsoleUserInteractor(IPlayerController controller)
        {
            PlayerMoveController = controller;
        }

        #endregion

        #region Members

        IPlayerController PlayerMoveController { get; }

        #endregion

        #region Say.

        public void SayWelcome()
        {
            Say("Hello! Welcome to Cavern of time game!");
            Say("In this game you'll have to find fountain and come back alive!");
            Say("Good luck!");
        }

        public void SayGoodbye()
        {
            Say("Thank you for playing!");
        }

        public void Say(string message)
        {
            Console.WriteLine(message);
        }

        public void SayError(string errorMessage)
        {
            Console.WriteLine($"Something went wrong, reason: {errorMessage}");
        }

        public void StepEnd()
        {
            Console.Clear();
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
            string availableDirections = string.Join(", ", PlayerMoveController.Keybindings.Keys);

            Console.Write($"Move! ({availableDirections}).");
            PlayerAction action = PlayerMoveController.WaitPlayerAction();

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
