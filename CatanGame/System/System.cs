using CatanGame.Enums;
using CatanGame.Models;
using CatanGame.UI;

namespace CatanGame.System;

public class System : ISystem
{
    private IBoard _board;
    private Dictionary<EPlayer, ICards> _allCards;
    private IUi _ui;
    private int _dice = 0;

    public System(IBoard board, Dictionary<EPlayer, ICards> allCards, IUi ui)
    {
        _board = board;
        _allCards = allCards;
        _ui = ui;
    }

    public void RoleDice()
    {
        var random = new Random();
        var cube1 = random.Next(1, 6);
        var cube2 = random.Next(1, 6);
        _dice = cube1 + cube2;
        _ui.ShowDice(cube1, cube2);
    }

    public IBoard GetBoard()
    {
        return _board;
    }

    public ICards GetCards(EPlayer ePlayer)
    {
        return _allCards[ePlayer];
    }

    public Dictionary<EPlayer, ICards> GetAllCards()
    {
        return _allCards;
    }

    public void GetGameSummery()
    {
        var showStatusApi = new ShowStatusApi
        {
            TotalPoints = new Dictionary<EPlayer, int>()
        };
        foreach (var (ePlayer, cards) in _allCards)
        {
            showStatusApi.TotalPoints[ePlayer] = cards.GetTotalPoints();
        }

        _ui.ShowStatus(showStatusApi);
    }

    public void BuildSettlement(int x, int y, EPlayer ePlayer)
    {
        if (_board.GetVertexOwner(x, y) != EPlayer.None)
        {
            throw new Exception("Can't build a settlement on owned vertex!");
        }

        if (_board.GetVertexStatus(x, y) != EVertexStatus.Unsettled)
        {
            throw new Exception("Can build a settlement only on unsettled vertex!");
        }
        
        _allCards[ePlayer].TransferResources(EResource.Sheep, -1);
        _allCards[ePlayer].TransferResources(EResource.Wood, -1);
        _allCards[ePlayer].TransferResources(EResource.Wheat, -1);
        _allCards[ePlayer].TransferResources(EResource.Tin, -1);
        
        _board.SetVertexOwner(x, y, ePlayer);
        _board.SetVertexStatus(x, y, EVertexStatus.Settlement);
        GetCards(ePlayer).TransferTotalPoints(1);
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