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


        public string KeybindingsHelp()
        {
            return string.Join("\n", Keybindings.Select(kb => $"{kb.Key} => {kb.Value.ToString()!.Split('.')[^1]}"));
        }
    }
}