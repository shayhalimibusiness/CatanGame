using CatanGame.Enums;
using CatanGame.System;

namespace CatanGame.Utils;

public class TestUtils
{
    private ISystem _system;
    
    public TestUtils(ISystem system)
    {
        _system = system;
    }
        
    public void TransferSettlementResources(EPlayer ePlayer)
    {
        var cards = _system.GetCards(ePlayer);
        cards.TransferResources(EResource.Sheep, 1);
        cards.TransferResources(EResource.Wood, 1);
        cards.TransferResources(EResource.Wheat, 1);
        cards.TransferResources(EResource.Tin, 1);
    }
    
    public void TransferCityResources(EPlayer ePlayer)
    {
        var cards = _system.GetCards(ePlayer);
        cards.TransferResources(EResource.Wheat, 2);
        cards.TransferResources(EResource.Iron, 3);
    }
    
    public void TransferRoadResources(EPlayer ePlayer)
    {
        var cards = _system.GetCards(ePlayer);
        cards.TransferResources(EResource.Wood, 1);
        cards.TransferResources(EResource.Tin, 1);
    }
    
    public void TransferPointCardResources(EPlayer ePlayer)
    {
        var cards = _system.GetCards(ePlayer);
        cards.TransferResources(EResource.Sheep, 1);
        cards.TransferResources(EResource.Wheat, 1);
        cards.TransferResources(EResource.Iron, 1);
    }
}