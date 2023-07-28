using CatanGame.Enums;
using CatanGame.System;

namespace CatanGameTests.SystemTests;

public class SystemTests
{
    private ISystem _system;
    
    [SetUp]
    public void Setup()
    {
        _system = SystemFactory.Create2PlayersSystem();
    }

    [Test]
    public void GetBoard_BlankBoard_FirstVertexIsUnsettled()
    {
        var board = _system.GetBoard();
        Assert.That(board.GetVertexStatus(0,0), Is.EqualTo(EVertexStatus.Unsettled));
    }
    
    [Test]
    public void GetCards_BlankPlayer_HasZeroTotalPoints()
    {
        var cards = _system.GetCards(EPlayer.Player1);
        Assert.That(cards.GetTotalPoints(), Is.EqualTo(0));
    }
    
    [Test]
    public void GetAllCards_TwoPlayers_SecondHasZeroTotalPoints()
    {
        var allCards = _system.GetAllCards();
        Assert.That(allCards[EPlayer.Player2].GetTotalPoints(), Is.EqualTo(0));
    }
    
    [Test]
    public void BuildSettlement_NotEnoughResource_ThrowsException()
    {
        Assert.Throws<Exception>(() => _system.BuildSettlement(0, 0, EPlayer.Player1));
    }
    
    [Test]
    public void BuildSettlement_Player1Build_VertexOwnedByPlayer1()
    {
        var cards = _system.GetCards(EPlayer.Player1);
        TransferSettlementResources(EPlayer.Player1);
        
        _system.BuildSettlement(0, 0, EPlayer.Player1);
        
        var owner = _system.GetBoard().GetVertexOwner(0, 0);
        var status = _system.GetBoard().GetVertexStatus(0, 0);
        var points = cards.GetTotalPoints();
        var resources = cards.GetResources();
        
        Assert.That(owner, Is.EqualTo(EPlayer.Player1));
        Assert.That(status, Is.EqualTo(EVertexStatus.Settlement));
        Assert.That(points, Is.EqualTo(1));
        Assert.That(resources[EResource.Tin], Is.EqualTo(0));
    }
    
    [Test]
    public void BuildSettlementUndo_UnsettledVertex_ThrowsException()
    {
        Assert.Throws<Exception>(() => _system.BuildSettlementUndo(0, 0, EPlayer.Player1));
    }
    
    [Test]
    public void BuildSettlementUndo_Player1BuildAndUndo_BackToNormal()
    {
        var cards = _system.GetCards(EPlayer.Player1);
        TransferSettlementResources(EPlayer.Player1);
        
        _system.BuildSettlement(0, 0, EPlayer.Player1);
        _system.BuildSettlementUndo(0, 0, EPlayer.Player1);

        var owner = _system.GetBoard().GetVertexOwner(0, 0);
        var status = _system.GetBoard().GetVertexStatus(0, 0);
        var points = cards.GetTotalPoints();
        var resources = cards.GetResources();
        
        Assert.That(owner, Is.EqualTo(EPlayer.None));
        Assert.That(status, Is.EqualTo(EVertexStatus.Unsettled));
        Assert.That(points, Is.EqualTo(0));
        Assert.That(resources[EResource.Tin], Is.EqualTo(1));
    }
    
    [Test]
    public void BuildCity_UnsettledVertex_ThrowsException()
    {
        Assert.Throws<Exception>(() => _system.BuildCity(0, 0, EPlayer.Player1));
    }
    
    [Test]
    public void BuildCity_Player1BuildCity_VertexHasPlayer1sCity()
    {
        var cards = _system.GetCards(EPlayer.Player1);
        TransferSettlementResources(EPlayer.Player1);
        _system.BuildSettlement(0, 0, EPlayer.Player1);
        TransferCityResources(EPlayer.Player1);
        
        _system.BuildCity(0, 0, EPlayer.Player1);
        
        var owner = _system.GetBoard().GetVertexOwner(0, 0);
        var status = _system.GetBoard().GetVertexStatus(0, 0);
        var points = cards.GetTotalPoints();
        var resources = cards.GetResources();
        
        Assert.That(owner, Is.EqualTo(EPlayer.Player1));
        Assert.That(status, Is.EqualTo(EVertexStatus.City));
        Assert.That(points, Is.EqualTo(2));
        Assert.That(resources[EResource.Wheat], Is.EqualTo(0));
    }
    
    [Test]
    public void BuildCityUndo_VertexContainsSettlement_ThrowsException()
    {
        TransferSettlementResources(EPlayer.Player1);
        Assert.Throws<Exception>(() => _system.BuildCityUndo(0, 0, EPlayer.Player1));
    }
    
    [Test]
    public void BuildCityUndo_Player1BuildAndUndo_VertexBackToNoraml()
    {
        var cards = _system.GetCards(EPlayer.Player1);
        TransferSettlementResources(EPlayer.Player1);
        TransferCityResources(EPlayer.Player1);
        _system.BuildSettlement(0, 0, EPlayer.Player1);
        _system.BuildCity(0, 0, EPlayer.Player1);
        
        _system.BuildCityUndo(0, 0, EPlayer.Player1);
                
        var owner = _system.GetBoard().GetVertexOwner(0, 0);
        var status = _system.GetBoard().GetVertexStatus(0, 0);
        var points = cards.GetTotalPoints();
        var resources = cards.GetResources();
        
        Assert.That(owner, Is.EqualTo(EPlayer.Player1));
        Assert.That(status, Is.EqualTo(EVertexStatus.Settlement));
        Assert.That(points, Is.EqualTo(1));
        Assert.That(resources[EResource.Wheat], Is.EqualTo(2));
    }

    [Test]
    public void BuildRoad_NotEnoughResources_ThrowsException()
    {
        Assert.Throws<Exception>(() => _system.BuildRoad(0, 0, ERoads.Horizontals, EPlayer.Player1));
    }
    
    [Test]
    public void BuildRoad_Player1Build_RoadOwnedByPlayer1()
    {
        var cards = _system.GetCards(EPlayer.Player1);
        TransferRoadResources(EPlayer.Player1);
        
        _system.BuildRoad(0, 0, ERoads.Horizontals, EPlayer.Player1);
        
        var owner = _system.GetBoard().GetRoadOwner(0, 0, ERoads.Horizontals);
        var resources = cards.GetResources();
        
        Assert.That(owner, Is.EqualTo(EPlayer.Player1));
        Assert.That(resources[EResource.Tin], Is.EqualTo(0));
    }
    
    [Test]
    public void BuildRoadUndo_UnsettledRoad_ThrowsException()
    {
        Assert.Throws<Exception>(() => _system.BuildRoadUndo(0, 0, ERoads.Horizontals, EPlayer.Player1));
    }
    
    [Test]
    public void BuildRoadUndo_Player1BuildAndUndo_BackToNormal()
    {
        var cards = _system.GetCards(EPlayer.Player1);
        TransferRoadResources(EPlayer.Player1);
        
        _system.BuildRoad(0, 0, ERoads.Horizontals, EPlayer.Player1);
        _system.BuildRoadUndo(0, 0, ERoads.Horizontals, EPlayer.Player1);

        var owner = _system.GetBoard().GetRoadOwner(0, 0, ERoads.Horizontals);
        var resources = cards.GetResources();
        
        Assert.That(owner, Is.EqualTo(EPlayer.None));
        Assert.That(resources[EResource.Tin], Is.EqualTo(1));
    }
    
    [Test]
    public void BuyCard_NotEnoughResources_ThrowsException()
    {
        Assert.Throws<Exception>(() => _system.BuyCard(EPlayer.Player1));
    }
    
    [Test]
    public void BuyCard_Player1Buy_SpendsResources()
    {
        var cards = _system.GetCards(EPlayer.Player1);
        TransferPointCardResources(EPlayer.Player1);
        
        _system.BuyCard(EPlayer.Player1);
        
        var resources = cards.GetResources();
        
        Assert.That(resources[EResource.Sheep], Is.EqualTo(0));
        Assert.That(resources[EResource.Iron], Is.EqualTo(0));
        Assert.That(resources[EResource.Wheat], Is.EqualTo(0));
    }
    
    [Test]
    public void BuyCardUndo_NoCardsWhereBoughtBefore_TrowsException()
    {
        Assert.Throws<Exception>(() => _system.BuyCardUndo(EPlayer.Player1));
    }
    
    [Test]
    public void BuyCardUndo_Player1BuyAndUndo_BackToNormal()
    {
        var cards = _system.GetCards(EPlayer.Player1);
        TransferPointCardResources(EPlayer.Player1);
        
        _system.BuyCard(EPlayer.Player1);
        _system.BuyCardUndo(EPlayer.Player1);

        var resources = cards.GetResources();
        
        Assert.That(resources[EResource.Sheep], Is.EqualTo(1));
        Assert.That(resources[EResource.Iron], Is.EqualTo(1));
        Assert.That(resources[EResource.Wheat], Is.EqualTo(1));
    }
    
    [Test]
    public void Trade_NotEnoughResources_ThrowsException()
    {
        Assert.Throws<Exception>(() => _system.Trade(EPlayer.Player1, EResource.Iron, EResource.Sheep, 1));
    }
    
    [Test]
    public void Trade_TradeIronForSheep_HasSheepNoIron()
    {
        var cards = _system.GetCards(EPlayer.Player1);
        cards.TransferResources(EResource.Iron, 4);
        
        _system.Trade(EPlayer.Player1, EResource.Iron, EResource.Sheep, 1);
        
        var resources = cards.GetResources();
        
        Assert.That(resources[EResource.Iron], Is.EqualTo(0));
        Assert.That(resources[EResource.Sheep], Is.EqualTo(1));
    }
    
    [Test]
    public void TradeUndo_NotEnoughResources_TrowsException()
    {
        Assert.Throws<Exception>(() => _system.TradeUndo(EPlayer.Player1, EResource.Iron, EResource.Sheep, 1));
    }
    
    [Test]
    public void TradeUndo_Player1TradeAndUndo_BackToNormal()
    {
        var cards = _system.GetCards(EPlayer.Player1);
        cards.TransferResources(EResource.Iron, 4);
        
        _system.Trade(EPlayer.Player1, EResource.Iron, EResource.Sheep, 1);
        _system.TradeUndo(EPlayer.Player1, EResource.Iron, EResource.Sheep, 1);
        
        var resources = cards.GetResources();
        
        Assert.That(resources[EResource.Iron], Is.EqualTo(4));
        Assert.That(resources[EResource.Sheep], Is.EqualTo(0));
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