using CatanGame.Enums;
using CatanGame.System.Board;
using CatanGame.Utils;

namespace CatanGame.System;

public static class SystemFactory
{
    public static ISystem CreateSystem()
    {
        var board = BoardFactory.CreateRandomBoard();
        var ui = UiFactory.CreateUi();
        var allCards = CardsFactory.CreateBlankAllCards();
        
        var system = new System(
            board: board,
            ui: ui,
            allCards: allCards);
        
        return system;
    }
}