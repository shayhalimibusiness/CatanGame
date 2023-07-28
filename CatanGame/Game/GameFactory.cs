using CatanGame.Enums;
using CatanGame.Player;
using CatanGame.System.Board;
using CatanGame.System.Cards;
using CatanGame.Utils;

namespace CatanGame.Game;

public static class GameFactory
{
    public static IGame Create1PlayerGame()
    {
        var board = BoardFactory.CreateRandomBoard();
        var ui = UiFactory.CreateUi();
        var allCards = CardsFactory.Create1PlayerBlankAllCards();
        var system = new System.System(board, allCards, ui);
        var evaluator = new Evaluator.Evaluator(system, EPlayer.Player1);
        var judge = new Judge.Judge(system, ui, EPlayer.Player1);
        var player = new HeuristicPlayer(judge, evaluator);

        var game = new Game(system, ui, new[] { player });

        return game;
    }
}