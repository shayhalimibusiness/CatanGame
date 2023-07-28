using CatanGame.Action;
using CatanGame.Enums;
using CatanGame.System;
using CatanGame.UI;
using CatanGame.Utils;

namespace CatanGame.Judge;

public class Judge : IJudge
{
    private readonly ISystem _system;
    private readonly IUi _ui;
    private readonly EPlayer _ePlayer;

    public Judge(ISystem system, IUi ui, EPlayer ePlayer)
    {
        _system = system;
        _ui = ui;
        _ePlayer = ePlayer;
    }
    
    public List<IAction> GetActions()
    {
        var actions = new List<IAction>();
        
        actions.AddRange(GetBuyCardAction());
        actions.AddRange(GetBuildRoadActions());
        actions.AddRange(GetBuildSettlementActions());
        actions.AddRange(GetBuildCityActions());
        actions.AddRange(GetTradeActions());
        
        return actions;
    }

    private List<IAction> GetBuyCardAction()
    {
        var actions = new List<IAction>();

        if (CanBuyCard())
        {
            actions.Add(new BuyCard(_system, _ui, _ePlayer));
        }

        return actions;
    }

    private List<IAction> GetBuildRoadActions()
    {
        var actions = new List<IAction>();

        if (!CanBuildRoad())
        {
            return actions;
        }

        var eligibleRoads = GetEligibleRoads();

        actions.AddRange(
            from road in eligibleRoads 
            let x = road.Item1 
            let y = road.Item2 
            let eRoad = road.Item3 
            select new BuildRoad(_system, _ui, x, y, eRoad, _ePlayer));

        return actions;
    }

    private List<IAction> GetBuildSettlementActions()
    {
        var actions = new List<IAction>();

        if (!CanBuildSettlement())
        {
            return actions;
        }

        var eligibleVertices = GetEligibleVertices();

        actions.AddRange(
            from vertex in eligibleVertices 
            let x = vertex.Item1 
            let y = vertex.Item2 
            select new BuildSettlement(_system, x, y, _ePlayer, _ui));

        return actions;
    }
    
    private List<IAction> GetBuildCityActions()
    {
        var actions = new List<IAction>();

        if (!CanBuildCity())
        {
            return actions;
        }

        var settlements = GetSettlements();
        actions.AddRange(
            from settlement in settlements
            let x = settlement.Item1 
            let y = settlement.Item2 
            select new BuildCity(_system, _ui, x, y, _ePlayer));

        return actions;
    }

    private List<IAction> GetTradeActions()
    {
        return (
            from resourceToSell in GlobalResources.Resources 
            where CanSellResource(resourceToSell) 
            from resourceToBuy in GlobalResources.Resources 
            where resourceToBuy != resourceToSell 
            select new Trade(_system, _ui, _ePlayer, resourceToSell, resourceToBuy, 1))
            .Cast<IAction>()
            .ToList();
    }

    private bool CanBuyCard()
    {
        var resources = _system.GetCards(_ePlayer).GetResources();
        var iron = resources[EResource.Iron];
        var sheep = resources[EResource.Sheep];
        var wheat = resources[EResource.Wheat];

        return iron >= 1 && sheep >= 1 && wheat >= 1;
    }
    
    private bool CanBuildSettlement()
    {
        var resources = _system.GetCards(_ePlayer).GetResources();
        var tin = resources[EResource.Tin];
        var wood = resources[EResource.Wood];
        var sheep = resources[EResource.Sheep];
        var wheat = resources[EResource.Wheat];

        return tin >= 1 && sheep >= 1 && wheat >= 1 && wood >= 1;
    }
    
    private bool CanBuildCity()
    {
        var resources = _system.GetCards(_ePlayer).GetResources();
        var iron = resources[EResource.Iron];
        var wheat = resources[EResource.Wheat];

        return iron >= 3 && wheat >= 2;
    }
    
    private bool CanBuildRoad()
    {
        var resources = _system.GetCards(_ePlayer).GetResources();
        var tin = resources[EResource.Tin];
        var wood = resources[EResource.Wood];

        return tin >= 1 && wood >= 1;
    }
    
    private bool CanSellResource(EResource eResource)
    {
        return _system.GetCards(_ePlayer).GetResources()[eResource] >= GetMarketRate(eResource);
    }

    private int GetMarketRate(EResource eResource)
    {
        var cards = _system.GetCards(_ePlayer);
        if (cards.HasPort(eResource))
        {
            return 2;
        } 
        return cards.HasPort(EResource.PointCard) ? 3 : 4;
    }

    private List<(int, int, ERoads)> GetEligibleRoads()
    {
        var eligibleRoads = new List<(int, int, ERoads)>();
        foreach (ERoads eRoad in Enum.GetValues(typeof(ERoads)))
        {
            var roadsXSize = GetRoadsSize(0, eRoad);
            var roadsYSize = GetRoadsSize(1, eRoad);
            for (var i = 0; i < roadsXSize; i++)
            {
                for (var j = 0; j < roadsYSize; j++)
                {
                    if (IsValidRoad(i, j, eRoad))
                    {
                        eligibleRoads.Add((i, j, eRoad));
                    }
                }
            }
        }

        return eligibleRoads;
    }

    private bool IsValidRoad(int x, int y, ERoads eRoad)
    {
        if (!IsRoadInRange(x, y, eRoad))
        {
            return false;
        }

        var neighbors = GetNeighborRoadsToRoad(x, y, eRoad);

        return (
            from neighbor in neighbors 
            let nX = neighbor.Item1 
            let nY = neighbor.Item2 
            let nERoad = neighbor.Item3
            let board = _system.GetBoard()
            where IsRoadInRange(nX, nY, nERoad) 
            where board.GetRoadOwner(nX, nY, nERoad) == _ePlayer 
            select nX
            ).Any();
    }

    private bool IsRoadInRange(int x, int y, ERoads eRoad)
    {
        var roadsXSize = GetRoadsSize(0, eRoad);
        var roadsYSize = GetRoadsSize(1, eRoad);
        return x >= 0 && x < roadsXSize && y >= 0 && y < roadsYSize;
    }

    private List<(int, int, ERoads)> GetNeighborRoadsToRoad(int x, int y, ERoads eRoads)
    {
        return eRoads switch
        {
            ERoads.Horizontals => new List<(int, int, ERoads)>
            {
                (x, y-1, ERoads.Horizontals),
                (x, y+1, ERoads.Horizontals),
                (x-1, y, ERoads.Verticals),
                (x-1, y+1, ERoads.Verticals),
                (x, y, ERoads.Verticals),
                (x, y+1, ERoads.Verticals),
            },
            ERoads.Verticals => new List<(int, int, ERoads)>
            {
                (x-1, y, ERoads.Verticals),
                (x+1, y, ERoads.Verticals),
                (x, y-1, ERoads.Horizontals),
                (x, y, ERoads.Horizontals),
                (x+1, y-1, ERoads.Horizontals),
                (x+1, y, ERoads.Horizontals),
            },
            _ => throw new ArgumentOutOfRangeException(nameof(eRoads), eRoads, null)
        };
    }

    private int GetRoadsSize(int dimension, ERoads eRoad)
    {
        return dimension switch
        {
            0 => eRoad switch
            {
                ERoads.Horizontals => GlobalResources.HorizontalsRoadsXSize,
                ERoads.Verticals => GlobalResources.VerticalsRoadsXSize,
                _ => throw new ArgumentOutOfRangeException()
            },
            1 => eRoad switch
            {
                ERoads.Horizontals => GlobalResources.HorizontalsRoadsYSize,
                ERoads.Verticals => GlobalResources.VerticalsRoadsYSize,
                _ => throw new ArgumentOutOfRangeException()
            },
            _ => throw new ArgumentOutOfRangeException(nameof(dimension), dimension, null)
        };
    }
    
    private List<(int, int)> GetEligibleVertices()
    {
        var eligibleVertices = new List<(int, int)>();
        for (var i = 0; i < GlobalResources.VerticesSize; i++)
        {
            for (var j = 0; j < GlobalResources.VerticesSize; j++)
            {
                if (IsValidVertex(i, j))
                {
                    eligibleVertices.Add((i, j));
                }
            }
        }

        return eligibleVertices;
    }
    
    private bool IsValidVertex(int x, int y)
    {
        if (!IsVertexInRange(x, y) || _system.GetBoard().GetVertexOwner(x, y) != EPlayer.None)
        {
            return false;
        }

        var neighborRoads = GetNeighborRoadsToVertex(x, y);
        var neighborVertices = GetNeighborVerticesToVertex(x, y);

        var isTouchingOwnedRoad = (
            from neighbor in neighborRoads 
            let nX = neighbor.Item1 
            let nY = neighbor.Item2 
            let nERoad = neighbor.Item3
            let board = _system.GetBoard()
            where IsRoadInRange(nX, nY, nERoad) 
            where board.GetRoadOwner(nX, nY, nERoad) == _ePlayer 
            select nX
            ).Any();

        var isFarFromSettlements = !(
            from neighbor in neighborVertices
            let nX = neighbor.Item1
            let nY = neighbor.Item2
            let board = _system.GetBoard()
            where IsVertexInRange(nX, nY) 
            where board.GetVertexOwner(nX, nY) != EPlayer.None 
            select nX
            ).Any();

        return isTouchingOwnedRoad && isFarFromSettlements;
    }
    
    private bool IsVertexInRange(int x, int y)
    {
        return x is >= 0 and < GlobalResources.VerticesSize && y is >= 0 and < GlobalResources.VerticesSize;
    }
    
    private List<(int, int, ERoads)> GetNeighborRoadsToVertex(int x, int y)
    {
        return new List<(int, int, ERoads)>
        {
            (x, y-1, ERoads.Horizontals),
            (x, y, ERoads.Horizontals),
            (x-1, y, ERoads.Verticals),
            (x, y, ERoads.Verticals),
        };
    }
    
    private List<(int, int)> GetNeighborVerticesToVertex(int x, int y)
    {
        return new List<(int, int)>
        {
            (x, y-1),
            (x, y+1),
            (x-1, y),
            (x+1, y),
        };
    }

    private List<(int, int)> GetSettlements()
    {
        var settlements = new List<(int, int)>();
        var board = _system.GetBoard();
        for (var i = 0; i < GlobalResources.VerticesSize; i++)
        {
            for (var j = 0; j < GlobalResources.VerticesSize; j++)
            {
                if (board.GetVertexOwner(i, j) == _ePlayer && 
                    board.GetVertexStatus(i, j) == EVertexStatus.Settlement)
                {
                    settlements.Add((i, j));
                }
            }
        }

        return settlements;
    }
}