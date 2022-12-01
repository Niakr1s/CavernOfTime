using CavernOfTime;
using CavernOfTime.ConsoleGame;

var game = new CavernGame(new StandardRules(), new ConsoleUserInteractor());
game.Start();