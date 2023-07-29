using CatanGame.Enums;
using CatanGame.System.Board;
using CatanGame.System.Cards;
using CatanGame.UI;
using CatanGame.Utils;

namespace CatanGame.System;

public class System : ISystem
{
    private readonly IBoard _board;
    private readonly Dictionary<EPlayer, ICards> _allCards;
    private readonly IUi _ui;
    private int _dice = 0;
    private readonly Dictionary<EPlayer, Stack<bool>> _pointCardsDrawsHistory;

    public System(IBoard board, Dictionary<EPlayer, ICards> allCards, IUi ui)
    {
        _board = board;
        _allCards = allCards;
        _ui = ui;
        _pointCardsDrawsHistory = new Dictionary<EPlayer, Stack<bool>>();
        foreach (var ePlayer in allCards.Keys)
        {
            _pointCardsDrawsHistory[ePlayer] = new Stack<bool>();
        }
    }

    public void RoleDice()
    {
        var random = new Random();
        var cube1 = random.Next(1, 6);
        var cube2 = random.Next(1, 6);
        _dice = cube1 + cube2;
        DistributeResources();
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

    public void SetSettlement(int x, int y, EPlayer ePlayer)
    {
        if (_board.GetVertexOwner(x, y) != EPlayer.None)
        {
            throw new Exception("Can't build a settlement on owned vertex!");
        }

        if (_board.GetVertexStatus(x, y) != EVertexStatus.Unsettled)
        {
            throw new Exception("Can build a settlement only on unsettled vertex!");
        }

        var cards = _allCards[ePlayer];

        _board.SetVertexOwner(x, y, ePlayer);
        _board.SetVertexStatus(x, y, EVertexStatus.Settlement);
        GetCards(ePlayer).TransferTotalPoints(1);
        
        var port = _board.PlayerHasPortIn(ePlayer, x, y);
        if (port != EResource.None)
        {
            cards.AddPort(port);
        }
    }

    public void SetSettlementUndo(int x, int y, EPlayer ePlayer)
    {
        if (_board.GetVertexOwner(x, y) != ePlayer)
        {
            throw new Exception("Can't revert build settlement! The settlement is not belonged to the player.");
        }

        if (_board.GetVertexStatus(x, y) != EVertexStatus.Settlement)
        {
            throw new Exception("Can't revert build settlement! There is no settlement in this place");
        }
        
        var cards = _allCards[ePlayer];
        
        _board.SetVertexOwner(x, y, EPlayer.None);
        _board.SetVertexStatus(x, y, EVertexStatus.Unsettled);
        GetCards(ePlayer).TransferTotalPoints(-1);
        
        var port = _board.PlayerHasPortIn(ePlayer, x, y);
        if (port != EResource.None)
        {
            cards.RemovePort(port);
        }
    }

    public void BuildSettlement(int x, int y, EPlayer ePlayer)
    {
        SetSettlement(x, y, ePlayer);

        var cards = _allCards[ePlayer];
        cards.TransferResources(EResource.Sheep, -1);
        cards.TransferResources(EResource.Wood, -1);
        cards.TransferResources(EResource.Wheat, -1);
        cards.TransferResources(EResource.Tin, -1);
    }

    public void BuildSettlementUndo(int x, int y, EPlayer ePlayer)
    {
        SetSettlementUndo(x, y, ePlayer);
        
        var cards = _allCards[ePlayer];
        cards.TransferResources(EResource.Sheep, 1);
        cards.TransferResources(EResource.Wood, 1);
        cards.TransferResources(EResource.Wheat, 1);
        cards.TransferResources(EResource.Tin, 1);
    }

    public void BuildCity(int x, int y, EPlayer ePlayer)
    {
        if (_board.GetVertexOwner(x, y) != ePlayer)
        {
            throw new Exception("Can't build a city! Player doesn't own a settlement here.");
        }

        if (_board.GetVertexStatus(x, y) != EVertexStatus.Settlement)
        {
            throw new Exception("Can build a city only on a settlement!");
        }
        
        _allCards[ePlayer].TransferResources(EResource.Wheat, -2);
        _allCards[ePlayer].TransferResources(EResource.Iron, -3);
        
        _board.SetVertexStatus(x, y, EVertexStatus.City);
        GetCards(ePlayer).TransferTotalPoints(1);
    }

    public void BuildCityUndo(int x, int y, EPlayer ePlayer)
    {
        if (_board.GetVertexOwner(x, y) != ePlayer)
        {
            throw new Exception("Can't undo build a city! Player doesn't own a settlement here.");
        }

        if (_board.GetVertexStatus(x, y) != EVertexStatus.City)
        {
            throw new Exception("Can't undo build a city! There isn't a city here.");
        }
        
        _allCards[ePlayer].TransferResources(EResource.Wheat, 2);
        _allCards[ePlayer].TransferResources(EResource.Iron, 3);
        
        _board.SetVertexStatus(x, y, EVertexStatus.Settlement);
        GetCards(ePlayer).TransferTotalPoints(-1);
    }

    public void SetRoad(int x, int y, ERoads eRoads, EPlayer ePlayer)
    {
        if (_board.GetRoadOwner(x, y, eRoads) != EPlayer.None)
        {
            throw new Exception("Can't build a road here! it is already owned by someone else.");
        }

        _board.SetRoadOwner(x, y, eRoads, ePlayer);
    }

    public void SetRoadUndo(int x, int y, ERoads eRoads, EPlayer ePlayer)
    {
        if (_board.GetRoadOwner(x, y, eRoads) != ePlayer)
        {
            throw new Exception("Can't revert build road! The road is not belonged to the player.");
        }

        _board.SetRoadOwner(x, y, eRoads, EPlayer.None);
    }

    public void BuildRoad(int x, int y, ERoads eRoads, EPlayer ePlayer)
    {
        SetRoad(x, y, eRoads, ePlayer);
        
        _allCards[ePlayer].TransferResources(EResource.Wood, -1);
        _allCards[ePlayer].TransferResources(EResource.Tin, -1);
    }

    public void BuildRoadUndo(int x, int y, ERoads eRoads, EPlayer ePlayer)
    {
        SetRoadUndo(x, y, eRoads, ePlayer);
        
        _allCards[ePlayer].TransferResources(EResource.Wood, 1);
        _allCards[ePlayer].TransferResources(EResource.Tin, 1);
    }

    public bool BuyCard(EPlayer ePlayer)
    {
        _allCards[ePlayer].TransferResources(EResource.Iron, -1);
        _allCards[ePlayer].TransferResources(EResource.Wheat, -1);
        _allCards[ePlayer].TransferResources(EResource.Sheep, -1);

        var isPoint = IsPointCard();

        if (!_pointCardsDrawsHistory.ContainsKey(ePlayer))
        {
            _pointCardsDrawsHistory[ePlayer] = new Stack<bool>();
        }

        var drawHistory = _pointCardsDrawsHistory[ePlayer];
        drawHistory.Push(isPoint);

        if (isPoint)
        {
            _allCards[ePlayer].TransferTotalPoints(1);
        }

        return isPoint;
    }

    public void BuyCardUndo(EPlayer ePlayer)
    {
        _allCards[ePlayer].TransferResources(EResource.Iron, 1);
        _allCards[ePlayer].TransferResources(EResource.Wheat, 1);
        _allCards[ePlayer].TransferResources(EResource.Sheep, 1);

        var isPoint = false;
        try
        {
            var drawHistory = _pointCardsDrawsHistory[ePlayer];
            isPoint = drawHistory.Pop();
        }
        catch (Exception e)
        {
            throw new Exception("The player didn't bought a card yet!");
        }

        if (isPoint)
        {
            _allCards[ePlayer].TransferTotalPoints(-1);
        }
    }

    public void Trade(EPlayer ePlayer, EResource sell, EResource buy, int times)
    {
        var marketRate = 4;
        _allCards[ePlayer].TransferResources(sell, -times * marketRate);
        _allCards[ePlayer].TransferResources(buy, times);
    }

    public void TradeUndo(EPlayer ePlayer, EResource sell, EResource buy, int times)
    {
        var marketRate = 4;
        _allCards[ePlayer].TransferResources(sell, times * marketRate);
        _allCards[ePlayer].TransferResources(buy, -times);
    }
    
    public bool HasPort(EPlayer ePlayer, EResource eResource)
    {
        return _board.PlayerOwnPort(ePlayer, eResource);
    }

    public int GetCardsBoughtByPlayer(EPlayer ePlayer)
    {
        return _pointCardsDrawsHistory[ePlayer].Count;
    }

    private bool IsPointCard()
    {
        var random = new Random();
        var randomNumber = random.Next(2);
        return randomNumber == 1;
    }

    private void DistributeResources()
    {
        var neighborVerticesOffset = new[] { (0, 0), (0, 1), (1, 0), (1, 1) }; 
        for (var i = 0; i < GlobalResources.TilesSize; i++)
        {
            for (var j = 0; j < GlobalResources.TilesSize; j++)
            {
                if (_board.GetTileNumber(i, j) != _dice || _board.GetTileResource(i, j) == EResource.None)
                {
                    continue;
                }
                
                foreach (var offset in neighborVerticesOffset)
                {
                    var (x, y) = offset;
                    if (_board.GetVertexStatus(i + x, j + y) == EVertexStatus.Settlement)
                    {
                        GetCards(_board.GetVertexOwner(i + x, j + y))
                            .TransferResources(_board.GetTileResource(i + x, j + y), 1);
                    }
                    if (_board.GetVertexStatus(i + x, j + y) == EVertexStatus.City)
                    {
                        GetCards(_board.GetVertexOwner(i + x, j + y))
                            .TransferResources(_board.GetTileResource(i + x, j + y), 2);
                    }
                }
            }
        }
    }
}