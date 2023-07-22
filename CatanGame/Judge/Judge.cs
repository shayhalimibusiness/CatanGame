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
    
    
}