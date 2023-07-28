using CatanGame.Enums;
using CatanGame.System;
using CatanGame.System.Board;
using CatanGame.System.Cards;
using CatanGame.Utils;

namespace CatanGame.Player;

public static class PlayerFactory
{
    public static (IPlayer, ISystem) CreateHeuristicPlayerLinkSystem(EPlayer ePlayer)
    {
        var board = BoardFactory.CreateRandomBoard();
        var ui = UiFactory.CreateUi();
        var allCards = CardsFactory.CreateBlankAllCards();
        var system = new System.System(board, allCards, ui);
        var evaluator = new Evaluator.Evaluator(system, ePlayer);
        var judge = new Judge.Judge(system, ui, ePlayer);

        var player = new HeuristicPlayer(judge, evaluator);

        return (player, system);
    }
}