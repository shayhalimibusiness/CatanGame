using CatanGame.Enums;
using CatanGame.System;

namespace CatanGame.Utils;

public class TestUtils
{
    private ISystem _system;
    
    TestUtils(ISystem system)
    {
        _system = system;
    }
        
    private void TransferSettlementResources(EPlayer ePlayer)
    {
        var cards = _system.GetCards(ePlayer);
        cards.TransferResources(EResource.Sheep, 1);
        cards.TransferResources(EResource.Wood, 1);
        cards.TransferResources(EResource.Wheat, 1);
        cards.TransferResources(EResource.Tin, 1);
    }
    
    private void TransferCityResources(EPlayer ePlayer)
    {
        var cards = _system.GetCards(ePlayer);
        cards.TransferResources(EResource.Wheat, 2);
        cards.TransferResources(EResource.Iron, 3);
    }
    
    private void TransferRoadResources(EPlayer ePlayer)
    {
        var cards = _system.GetCards(ePlayer);
        cards.TransferResources(EResource.Wood, 1);
        cards.TransferResources(EResource.Tin, 1);
    }
    
    private void TransferPointCardResources(EPlayer ePlayer)
    {
        var cards = _system.GetCards(ePlayer);
        cards.TransferResources(EResource.Sheep, 1);
        cards.TransferResources(EResource.Wheat, 1);
        cards.TransferResources(EResource.Iron, 1);
    }
}