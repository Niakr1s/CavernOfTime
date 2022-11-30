using CavernOfTime.ConsoleGame;

namespace CavernOfTime.ConsoleCavernGame
{
    public class ConsoleCavernGame : CavernGame
    {
        public ConsoleCavernGame(IRules rules)
            : base(
                  new ConsoleUserInteractor(ConsoleKeyboardController.WsadAndArrows()),
                  new ConsoleCavernDisplayer(),
                  ConsoleKeyboardController.WsadAndArrows(),
                  rules
                  )
        {
        }
    }
}
