using CatanGame.Action;
using CatanGame.Enums;
using CatanGame.System;
using CatanGame.UI;

namespace CatanGame.Judge;

public class Judge : IJudge
{
    private ISystem _system;
    private IUi _ui;
    private EPlayer _ePlayer;

    Judge(ISystem system, IUi ui, EPlayer ePlayer)
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
        var resources = _system.GetCards(_ePlayer).GetResources();
        var iron = resources[EResource.Iron];
        var sheep = resources[EResource.Sheep];
        var wheat = resources[EResource.Wheat];
        
        if (iron > 1 && sheep > 1 && wheat > 1)
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

    
    private bool CanBuildSettlement()
    {
        var resources = _system.GetCards(_ePlayer).GetResources();
        var iron = resources[EResource.Iron];
        var sheep = resources[EResource.Sheep];
        var wheat = resources[EResource.Wheat];

        return iron > 1 && sheep > 1 && wheat > 1;
    }
    
    private bool CanBuildCity()
    {
        var resources = _system.GetCards(_ePlayer).GetResources();
        var iron = resources[EResource.Iron];
        var sheep = resources[EResource.Sheep];
        var wheat = resources[EResource.Wheat];

        return iron > 1 && sheep > 1 && wheat > 1;
    }
    
    private bool CanBuildRoad()
    {
        var resources = _system.GetCards(_ePlayer).GetResources();
        var iron = resources[EResource.Iron];
        var sheep = resources[EResource.Sheep];
        var wheat = resources[EResource.Wheat];

        return iron > 1 && sheep > 1 && wheat > 1;
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

}