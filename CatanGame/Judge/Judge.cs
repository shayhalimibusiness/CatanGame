using System.Diagnostics;
using CatanGame.Action;
using CatanGame.Enums;
using CatanGame.System;
using CatanGame.UI;

namespace CatanGame.Judge;

public class Judge : IJudge
{
    private const int HorizontalsRoadsXSize = 5;
    private const int HorizontalsRoadsYSize = 4;
    private const int VerticalsRoadsXSize = 4;
    private const int VerticalsRoadsYSize = 5;
    
    private ISystem _system;
    private IUi _ui;
    private EPlayer _ePlayer;

    public Judge(ISystem system, IUi ui, EPlayer ePlayer)
    {
        _system = system;
        _ui = ui;
        _ePlayer = ePlayer;
    }
    
    public List<IAction> GetActions()
    {
        throw new NotImplementedException();
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

    // private List<IAction> GetBuildRoadActions()
    // {
    //     var actions = new List<IAction>();
    //
    //     if (CanBuildRoad())
    //     {
    //         foreach (var VARIABLE in COLLECTION)
    //         {
    //             actions.Add(new BuyCard(_system, _ui, _ePlayer));
    //         }
    //     }
    //
    //     return actions;
    // }

    private bool CanBuyCard()
    {
        var resources = _system.GetCards(_ePlayer).GetResources();
        var iron = resources[EResource.Iron];
        var sheep = resources[EResource.Sheep];
        var wheat = resources[EResource.Wheat];

        return iron > 1 && sheep > 1 && wheat > 1;
    }
    
    private bool CanBuildSettlement()
    {
        var resources = _system.GetCards(_ePlayer).GetResources();
        var tin = resources[EResource.Tin];
        var wood = resources[EResource.Wood];
        var sheep = resources[EResource.Sheep];
        var wheat = resources[EResource.Wheat];

        return tin > 1 && sheep > 1 && wheat > 1 && wood > 1;
    }
    
    private bool CanBuildCity()
    {
        var resources = _system.GetCards(_ePlayer).GetResources();
        var iron = resources[EResource.Iron];
        var wheat = resources[EResource.Wheat];

        return iron > 3 && wheat > 2;
    }
    
    private bool CanBuildRoad()
    {
        var resources = _system.GetCards(_ePlayer).GetResources();
        var tin = resources[EResource.Tin];
        var wood = resources[EResource.Wood];

        return tin > 1 && wood > 1;
    }
    
    private bool CanSellResource(EResource eResource)
    {
        var resources = _system.GetCards(_ePlayer).GetResources();
        var iron = resources[EResource.Iron];
        var sheep = resources[EResource.Sheep];
        var wheat = resources[EResource.Wheat];

        return iron > 1 && sheep > 1 && wheat > 1;
    }

    // private List<(int, int, ERoads)> GetEligibleRoads()
    // {
    //     var eligibleRoads = new List<(int, int, ERoads)>();
    //     foreach (ERoads eRoad in Enum.GetValues(typeof(ERoads)))
    //     {
    //         var roadsXSize = GetRoadsSize(0, eRoad);
    //         var roadsYSize = GetRoadsSize(1, eRoad);
    //         for (var i = 0; i < roadsXSize; i++)
    //         {
    //             for (var j = 0; j < roadsYSize; j++)
    //             {
    //                 if (_system.GetBoard().GetRoadOwner())
    //                 {
    //                     
    //                 }
    //             }
    //         }
    //     }
    // }

    // private bool IsValidRoad(int x, int y, ERoads eRoad)
    // {
    //     var roadsXSize = GetRoadsSize(0, eRoad);
    //     var roadsYSize = GetRoadsSize(1, eRoad);
    //     if (x < 0 || x >= roadsXSize || y < 0 || y >= roadsYSize)
    //     {
    //         return false;
    //     }
    //
    //     var neighbors = eRoad switch
    //     {
    //         ERoads.Horizontals => new List<(int, int)>
    //         {
    //             (x, y-1), (x, y+1), (x, y), (x-1, y+1), (x, y), (x, y+1)
    //         }
    //     };
    // }

    public List<(int, int, ERoads)> GetNeighborRoads(int x, int y, ERoads eRoads)
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
                ERoads.Horizontals => HorizontalsRoadsXSize,
                ERoads.Verticals => VerticalsRoadsXSize,
                _ => throw new ArgumentOutOfRangeException()
            },
            1 => eRoad switch
            {
                ERoads.Horizontals => HorizontalsRoadsYSize,
                ERoads.Verticals => VerticalsRoadsYSize,
                _ => throw new ArgumentOutOfRangeException()
            },
            _ => throw new ArgumentOutOfRangeException(nameof(dimension), dimension, null)
        };
    }
}