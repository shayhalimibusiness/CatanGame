using CatanGame.Enums;
using CatanGame.Models;

namespace CatanGame.System;

public class System : ISystem
{
    private IBoard _board;
    private Dictionary<EPlayer, ICards> _allCards;

    public System(IBoard board, Dictionary<EPlayer, ICards> allCards)
    {
        _board = board;
        _allCards = allCards;
    }

    public void MakeBoard()
    {
        throw new NotImplementedException();
    }

    public void RoleDice()
    {
        throw new NotImplementedException();
    }

    public IBoard GetBoard()
    {
        throw new NotImplementedException();
    }

    public ICards GetCards(EPlayer ePlayer)
    {
        throw new NotImplementedException();
    }

    public Dictionary<EPlayer, ICards> GetAllCards()
    {
        throw new NotImplementedException();
    }

    public GameSummaryApi GetGameSummery()
    {
        throw new NotImplementedException();
    }

    public void BuildSettlement(int x, int y, EPlayer ePlayer)
    {
        throw new NotImplementedException();
    }

    public void BuildSettlementUndo(int x, int y, EPlayer ePlayer)
    {
        throw new NotImplementedException();
    }

    public void BuildCity(int x, int y, EPlayer ePlayer)
    {
        throw new NotImplementedException();
    }

    public void BuildCityUndo(int x, int y, EPlayer ePlayer)
    {
        throw new NotImplementedException();
    }

    public void BuildRoad(int x, int y, ERoads eRoads, EPlayer ePlayer)
    {
        throw new NotImplementedException();
    }

    public void BuildRoadUndo(int x, int y, ERoads eRoads, EPlayer ePlayer)
    {
        throw new NotImplementedException();
    }

    public void BuyCard(EPlayer ePlayer)
    {
        throw new NotImplementedException();
    }

    public void BuyCardUndo(EPlayer ePlayer)
    {
        throw new NotImplementedException();
    }

    public void TradeUndo(EResource sell, EResource buy, int times)
    {
        throw new NotImplementedException();
    }
}