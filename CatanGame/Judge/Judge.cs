using CatanGame.Action;
using CatanGame.Enums;
using CatanGame.System;

namespace CatanGame.Judge;

public class Judge : IJudge
{
    private ISystem _system;
    private EPlayer _ePlayer;
    
    Judge(ISystem system, EPlayer ePlayer)
    {
        _system = system;
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
            actions.Add(new BuyCard(_system, _ePlayer));
        }

        return actions;
    }
    
    
}