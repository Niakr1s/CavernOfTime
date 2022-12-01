using CavernOfTime.PlayerActions;

namespace CavernOfTime.ConsoleGame
{
    internal class ConsoleKeyboardController
    {
        #region Constructors

        public ConsoleKeyboardController()
        {
        }

        public ConsoleKeyboardController(Dictionary<ConsoleKey, PlayerAction> keybindings)
        {
            Keybindings = keybindings;
        }

        #endregion


        #region Members

        public Dictionary<ConsoleKey, PlayerAction> Keybindings { get; } = new()
            {
                { ConsoleKey.W, new PlayerMoveAction(){ Direction = Direction.North } },
                { ConsoleKey.S, new PlayerMoveAction(){ Direction = Direction.South } },
                { ConsoleKey.A, new PlayerMoveAction(){ Direction = Direction.West } },
                { ConsoleKey.D, new PlayerMoveAction(){ Direction = Direction.East } },

                { ConsoleKey.UpArrow, new PlayerShootAction(){ Direction = Direction.North } },
                { ConsoleKey.DownArrow, new PlayerShootAction(){ Direction = Direction.South } },
                { ConsoleKey.LeftArrow, new PlayerShootAction(){ Direction = Direction.West } },
                { ConsoleKey.RightArrow, new PlayerShootAction(){ Direction = Direction.East } },

                {ConsoleKey.Enter , new PlayerInteractRequestAction() },
                {ConsoleKey.Spacebar , new PlayerInteractRequestAction() },
            };

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


        public string KeybindingsHelp()
        {
            return string.Join("\n", Keybindings.Select(kb => $"{kb.Key} => {kb.Value.ToString()!.Split('.')[^1]}"));
        }

        #region Helpers

        #endregion
    }
}