using CatanGame.Enums;
using CatanGame.System.Board;
using CatanGame.System.Cards;
using CatanGame.UI;
using CatanGame.Utils;

namespace CatanGame.System;

public static class SystemFactory
{
    public static ISystem? CreateSystemCopy(ISystem other)
    {
        return other is not System system ? null : new System(system);
    }
    public static ISystem Create2PlayersSystem()
    {
        var board = BoardFactory.CreateRandomBoard();
        var ui = UiFactory.CreateUi();
        var allCards = CardsFactory.Create2PlayersBlankAllCards();
        
        var system = new System(
            board: board,
            ui: ui,
            allCards: allCards);
        
        return system;
    }
    
    public static ISystem Create1PlayerSystem()
    {
        var board = BoardFactory.CreateRandomBoard();
        var ui = UiFactory.CreateUi();
        var allCards = CardsFactory.Create2PlayersBlankAllCards();
        
        var system = new System(
            board: board,
            ui: ui,
            allCards: allCards);
        
        return system;
    }
    
    public static ISystem CreateSystemAndLinqUi(IUi ui)
    {
        var board = BoardFactory.CreateRandomBoard();
        var allCards = CardsFactory.Create2PlayersBlankAllCards();
        
        var system = new System(
            board: board,
            ui: ui,
            allCards: allCards);
        
        return system;
    }
}