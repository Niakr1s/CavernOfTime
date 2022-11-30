namespace CavernOfTime.ConsoleGame
{
    public interface IPlayerController
    {
        Dictionary<ConsoleKey, PlayerAction> Keybindings { get; }

        /// <summary>
        /// Shouldn't throw exceptions.
        /// </summary>
        /// <returns></returns>
        PlayerAction WaitPlayerAction();
    }

    internal class ConsolePlayerMoveController : IPlayerController
    {
        #region Constructors

        private ConsolePlayerMoveController(
            Dictionary<ConsoleKey, PlayerAction> keybindings)
        {
            Keybindings = keybindings;
        }

        public static ConsolePlayerMoveController WsadAndArrows()
        {
            Dictionary<ConsoleKey, PlayerAction> directionKeybindings = new()
            {
                { ConsoleKey.W, new(){ Direction = Direction.North } },
                { ConsoleKey.S, new(){ Direction = Direction.South } },
                { ConsoleKey.A, new(){ Direction = Direction.West } },
                { ConsoleKey.D, new(){ Direction = Direction.East } },

                { ConsoleKey.UpArrow, new(){ Direction = Direction.North } },
                { ConsoleKey.DownArrow, new(){ Direction = Direction.South } },
                { ConsoleKey.LeftArrow, new(){ Direction = Direction.West } },
                { ConsoleKey.RightArrow, new(){ Direction = Direction.East } },

                {ConsoleKey.Enter , new() {WantInteract = true} },
                {ConsoleKey.Spacebar , new() {WantInteract = true} },
            };

            ConsoleKey[] interactKeybindings = new ConsoleKey[] { ConsoleKey.Enter, ConsoleKey.Spacebar };

            return new ConsolePlayerMoveController(directionKeybindings);
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