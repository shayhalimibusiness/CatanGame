using CatanGame.Action;
using CatanGame.Enums;
using CatanGame.Judge;
using CatanGame.System;
using CatanGame.System.Board;
using CatanGame.System.Cards;
using CatanGame.Utils;

namespace CatanGameTests.JudgeTests;

public class JudgeTests
{
    private ISystem _system;
    private IBoard _board;
    private ICards _player1Cards;
    private TestUtils _testUtils;
    private IJudge _judge;
    
    [SetUp]
    public void Setup()
    {
        var fullPackage = JudgeFactory.CreateJudgeFullReturn();
        _judge = fullPackage.Item1;
        _system = fullPackage.Item2;
        _board = _system.GetBoard();
        _player1Cards = _system.GetCards(EPlayer.Player1);
        _testUtils = new TestUtils(_system);
    }

    [Test]
    public void GetBuyCardAction_CanBuy_GetAction()
    {
        _testUtils.TransferPointCardResources(EPlayer.Player1);
        var actions = _judge.GetActions()
            .Where(action => action.GetType() == typeof(BuyCard));
        Assert.That(actions, !Is.Empty);
    }
    
    [Test]
    public void GetBuyCardAction_CantBuy_GetNoActions()
    {
        _testUtils.TransferCityResources(EPlayer.Player1);
        var actions = _judge.GetActions()
            .Where(action => action.GetType() == typeof(BuyCard));
        Assert.That(actions, Is.Empty);
    }
    
    [Test]
    public void GetBuildRoadActions_CanBuy_GetActions()
    {
        _testUtils.TransferRoadResources(EPlayer.Player1);
        _system.BuildRoad(2,2, ERoads.Horizontals, EPlayer.Player1);
        _testUtils.TransferRoadResources(EPlayer.Player1);

        var actions = _judge.GetActions()
            .Where(action => action.GetType() == typeof(BuildRoad));
        
        Assert.That(actions.Count(), Is.EqualTo(6));
    }
    
    [Test]
    public void GetBuildRoadAction_CantBuy_GetNoActions()
    {
        _testUtils.TransferCityResources(EPlayer.Player1);
        var actions = _judge.GetActions()
            .Where(action => action.GetType() == typeof(BuildRoad));
        Assert.That(actions, Is.Empty);
    }
    
    [Test]
    public void GetBuildRoadAction_NoEligibleRoads_GetNoActions()
    {
        _testUtils.TransferRoadResources(EPlayer.Player1);

        var actions = _judge.GetActions()
            .Where(action => action.GetType() == typeof(BuildRoad));
        
        Assert.That(actions, Is.Empty);
    }
    
    [Test]
    public void GetBuildSettlementActions_CanBuy_GetActions()
    {
        _testUtils.TransferSettlementResources(EPlayer.Player1);
        _system.BuildSettlement(2,2, EPlayer.Player1);
        for (var i = 0; i < 4; i++)
        {
            _testUtils.TransferRoadResources(EPlayer.Player1);
            _system.BuildRoad(2, i, ERoads.Horizontals, EPlayer.Player1);

        }
        
        _testUtils.TransferSettlementResources(EPlayer.Player1);
        
        var actions = _judge.GetActions()
            .Where(action => action.GetType() == typeof(BuildSettlement));
        
        Assert.That(actions.Count(), Is.EqualTo(2));
    }
    
    [Test]
    public void GetBuildSettlementActions_CantBuy_GetNoActions()
    {
        _testUtils.TransferCityResources(EPlayer.Player1);
        var actions = _judge.GetActions()
            .Where(action => action.GetType() == typeof(BuildSettlement));
        Assert.That(actions, Is.Empty);
    }
    
    [Test]
    public void GetBuildSettlementActions_NoEligibleVertices_GetNoActions()
    {
        _testUtils.TransferSettlementResources(EPlayer.Player1);

        var actions = _judge.GetActions()
            .Where(action => action.GetType() == typeof(BuildSettlement));
        
        Assert.That(actions, Is.Empty);
    }
    
    [Test]
    public void GetBuildCityActions_CanBuy_GetActions()
    {
        _testUtils.TransferSettlementResources(EPlayer.Player1);
        _system.BuildSettlement(2,2, EPlayer.Player1);

        _testUtils.TransferCityResources(EPlayer.Player1);
        
        var actions = _judge.GetActions()
            .Where(action => action.GetType() == typeof(BuildCity));
        
        Assert.That(actions.Count(), Is.EqualTo(1));
    }
    
    [Test]
    public void GetBuildCityActions_CantBuy_GetNoActions()
    {
        _testUtils.TransferRoadResources(EPlayer.Player1);
        var actions = _judge.GetActions()
            .Where(action => action.GetType() == typeof(BuildCity));
        Assert.That(actions, Is.Empty);
    }
    
    [Test]
    public void GetBuildCityActions_NoSettlements_GetNoActions()
    {
        _testUtils.TransferCityResources(EPlayer.Player1);

        var actions = _judge.GetActions()
            .Where(action => action.GetType() == typeof(BuildCity));
        
        Assert.That(actions, Is.Empty);
    }
    
    [Test]
    public void Trade_4Iron_CanTrade()
    {
        var cards = _system.GetCards(EPlayer.Player1);
        cards.TransferResources(EResource.Iron, 4);

        var actions = _judge.GetActions()
            .Where(action => action.GetType() == typeof(Trade));
        
        Assert.That(actions.Count(), Is.EqualTo(4));
    }
    
    [Test]
    public void Trade_3Iron_CantTrade()
    {
        var cards = _system.GetCards(EPlayer.Player1);
        cards.TransferResources(EResource.Iron, 3);

        var actions = _judge.GetActions()
            .Where(action => action.GetType() == typeof(Trade));
        
        Assert.That(actions, Is.Empty);
    }
    
    [Test]
    public void Trade_3IronAndGeneralPort_CanTrade()
    {
        _player1Cards.TransferResources(EResource.Iron, 3);
        _player1Cards.AddPort(EResource.PointCard);
        
        var actions = _judge.GetActions()
            .Where(action => action.GetType() == typeof(Trade));
        
        Assert.That(actions.Count(), Is.EqualTo(4));
    }
    
    [Test]
    public void Trade_2IronAndGeneralPort_CantTrade()
    {
        _player1Cards.TransferResources(EResource.Iron, 2);
        _player1Cards.AddPort(EResource.PointCard);
        
        var actions = _judge.GetActions()
            .Where(action => action.GetType() == typeof(Trade));
        
        Assert.That(actions, Is.Empty);
    }
    
    [Test]
    public void Trade_2IronAndIronPort_CanTrade()
    {
        _player1Cards.TransferResources(EResource.Iron, 2);
        _player1Cards.AddPort(EResource.Iron);
        
        var actions = _judge.GetActions()
            .Where(action => action.GetType() == typeof(Trade));
        
        Assert.That(actions.Count(), Is.EqualTo(4));
    }
}