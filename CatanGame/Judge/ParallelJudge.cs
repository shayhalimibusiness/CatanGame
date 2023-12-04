using CatanGame.Action;
using CatanGame.Enums;
using CatanGame.System;
using CatanGame.UI;
using CatanGame.Utils;

namespace CatanGame.Judge;

public class ParallelJudge : IJudge
{
    public readonly ISystem _system;
    private readonly IUi _ui;
    public readonly EPlayer _ePlayer;

    public ParallelJudge(ISystem system, IUi ui, EPlayer ePlayer)
    {
        _system = system;
        _ui = ui;
        _ePlayer = ePlayer;
    }
    
    public List<IAction> GetActions()
    {
        var actions = new List<IAction>
        {
            new Pass(_system)
        };
        
        actions.AddRange(GetBuyCardAction());
        actions.AddRange(GetBuildRoadActions());
        actions.AddRange(GetBuildSettlementActions());
        actions.AddRange(GetBuildCityActions());
        actions.AddRange(GetTradeActions());
        
        return actions;
    }

    public List<IAction> GetFirstSettlementsActions()
    {
        var actions = new List<IAction>();

        var eligibleVertices = GetEligibleFirstVertices();

        actions.AddRange(
            from vertex in eligibleVertices 
            let x = vertex.Item1 
            let y = vertex.Item2 
            select new SetSettlement(SystemFactory.CopySystem(_system)!, x, y, _ePlayer, _ui));

        return actions;
    }

    public List<IAction> GetFirstRoadsActions()
    {
        var actions = new List<IAction>();

        var eligibleRoads = GetEligibleFirstRoads();

        actions.AddRange(
            from road in eligibleRoads 
            let x = road.Item1 
            let y = road.Item2 
            let eRoad = road.Item3 
            select new SetRoad(SystemFactory.CopySystem(_system)!, _ui, x, y, eRoad, _ePlayer));

        return actions;
    }

    private List<IAction> GetBuyCardAction()
    {
        var actions = new List<IAction>();

        if (CanBuyCard())
        {
            actions.Add(new BuyCard(SystemFactory.CopySystem(_system)!, _ui, _ePlayer));
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
            select new BuildRoad(SystemFactory.CopySystem(_system)!, _ui, x, y, eRoad, _ePlayer));

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
            select new BuildSettlement(SystemFactory.CopySystem(_system)!, x, y, _ePlayer, _ui));

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
            select new BuildCity(SystemFactory.CopySystem(_system)!, _ui, x, y, _ePlayer));

        return actions;
    }

    private List<IAction> GetTradeActions()
    {
        return (
            from resourceToSell in GlobalResources.Resources 
            where CanSellResource(resourceToSell) 
            from resourceToBuy in GlobalResources.Resources 
            where resourceToBuy != resourceToSell 
            select new Trade(SystemFactory.CopySystem(_system)!, _ui, _ePlayer, resourceToSell, resourceToBuy, 1))
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
        return _system.GetMarketRate(_ePlayer, eResource);
    }

    private List<(int, int, ERoads)> GetEligibleRoads()
    {
        var eligibleRoads = new List<(int, int, ERoads)>();
        foreach (ERoads eRoad in Enum.GetValues(typeof(ERoads)))
        {
            var roadsXSize = GlobalResources.GetRoadsSize(0, eRoad);
            var roadsYSize = GlobalResources.GetRoadsSize(1, eRoad);
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
    
    private List<(int, int, ERoads)> GetEligibleFirstRoads()
    {
        var eligibleRoads = new List<(int, int, ERoads)>();
        foreach (ERoads eRoad in Enum.GetValues(typeof(ERoads)))
        {
            var roadsXSize = GlobalResources.GetRoadsSize(0, eRoad);
            var roadsYSize = GlobalResources.GetRoadsSize(1, eRoad);
            for (var i = 0; i < roadsXSize; i++)
            {
                for (var j = 0; j < roadsYSize; j++)
                {
                    if (IsValidFirstRoad(i, j, eRoad))
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
        if (!IsRoadInRange(x, y, eRoad) || _system.GetBoard().GetRoadOwner(x, y, eRoad) != EPlayer.None)
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

    private bool IsValidFirstRoad(int x, int y, ERoads eRoad)
    {
        if (!IsRoadInRange(x, y, eRoad) || _system.GetBoard().GetRoadOwner(x, y, eRoad) != EPlayer.None)
        {
            return false;
        }

        var neighbors = GetNeighborVerticesToRoad(x, y, eRoad);
        
        return (
            from neighbor in neighbors 
            let nX = neighbor.Item1 
            let nY = neighbor.Item2 
            let board = _system.GetBoard()
            where board.GetVertexOwner(nX, nY) == _ePlayer 
            select nX
        ).Any();
    }

    private List<(int, int)> GetNeighborVerticesToRoad(int x, int y, ERoads eRoads)
    {
        return eRoads switch
        {
            ERoads.Horizontals => new List<(int, int)> { (x, y), (x, y + 1) },
            ERoads.Verticals => new List<(int, int)> { (x, y), (x + 1, y) },
            _ => throw new ArgumentOutOfRangeException(nameof(eRoads), eRoads, null)
        };
    } 

    private bool IsRoadInRange(int x, int y, ERoads eRoad)
    {
        var roadsXSize = GlobalResources.GetRoadsSize(0, eRoad);
        var roadsYSize = GlobalResources.GetRoadsSize(1, eRoad);
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
        
        var neighborVertices = GetNeighborVerticesToVertex(x, y);
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
    
    private List<(int, int)> GetEligibleFirstVertices()
    {
        var eligibleVertices = new List<(int, int)>();
        for (var i = 0; i < GlobalResources.VerticesSize; i++)
        {
            for (var j = 0; j < GlobalResources.VerticesSize; j++)
            {
                if (IsValidFirstVertices(i, j))
                {
                    eligibleVertices.Add((i, j));
                }
            }
        }

        return eligibleVertices;
    }
    
    private bool IsValidFirstVertices(int x, int y)
    {
        if (_system.GetBoard().GetVertexOwner(x, y) != EPlayer.None)
        {
            return false;
        }

        var neighborVertices = GetNeighborVerticesToVertex(x, y);
        var isFarFromSettlements = !(
            from neighbor in neighborVertices
            let nX = neighbor.Item1
            let nY = neighbor.Item2
            let board = _system.GetBoard()
            where IsVertexInRange(nX, nY) 
            where board.GetVertexOwner(nX, nY) != EPlayer.None 
            select nX
        ).Any();

        return isFarFromSettlements;
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