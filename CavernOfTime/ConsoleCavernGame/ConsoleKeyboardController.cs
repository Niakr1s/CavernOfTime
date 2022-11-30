namespace CavernOfTime.ConsoleGame
{
    internal class ConsoleKeyboardController : IKeyboardController
    {
        #region Constructors

        private ConsoleKeyboardController(
            Dictionary<ConsoleKey, PlayerAction> keybindings)
        {
            Keybindings = keybindings;
        }

        public static ConsoleKeyboardController WsadAndArrows()
        {
            Dictionary<ConsoleKey, PlayerAction> directionKeybindings = new()
            {
                { ConsoleKey.W, new(){ Direction = Direction.North } },
                { ConsoleKey.S, new(){ Direction = Direction.South } },
                { ConsoleKey.A, new(){ Direction = Direction.West } },
                { ConsoleKey.D, new(){ Direction = Direction.East } },

                { ConsoleKey.UpArrow, new(){ ShootDirection = Direction.North } },
                { ConsoleKey.DownArrow, new(){ ShootDirection = Direction.South } },
                { ConsoleKey.LeftArrow, new(){ ShootDirection = Direction.West } },
                { ConsoleKey.RightArrow, new(){ ShootDirection = Direction.East } },

                {ConsoleKey.Enter , new() {WantInteract = true} },
                {ConsoleKey.Spacebar , new() {WantInteract = true} },
            };

            return new ConsoleKeyboardController(directionKeybindings);
        }

        #endregion


        #region Members

        public Dictionary<ConsoleKey, PlayerAction> Keybindings { get; }

        #endregion

        public PlayerAction WaitPlayerAction()
        {
            ConsoleKey key;
            do
            {
                key = Console.ReadKey().Key;
            } while (!Keybindings.ContainsKey(key));

            return Keybindings[key];
        }

        #region Helpers

        #endregion
    }
}