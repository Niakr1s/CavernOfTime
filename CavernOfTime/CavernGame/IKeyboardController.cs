namespace CavernOfTime
{
    public interface IKeyboardController
    {
        Dictionary<ConsoleKey, PlayerAction> Keybindings { get; }

        /// <summary>
        /// Shouldn't throw exceptions.
        /// </summary>
        /// <returns></returns>
        PlayerAction WaitPlayerAction();
    }
}