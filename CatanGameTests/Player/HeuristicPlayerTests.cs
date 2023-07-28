using CatanGame.Enums;
using CatanGame.Player;
using CatanGame.System;
using CatanGame.System.Board;
using CatanGame.System.Cards;
using CatanGame.Utils;

namespace CatanGameTests.Player;

public class HeuristicPlayerTests
{
    private IPlayer _player = null!;
    private ISystem _system = null!;
    private TestUtils _testUtils = null!;
    
    [SetUp]
    public void Setup()
    {
        (_player, _system) = PlayerFactory.CreateHeuristicPlayerLinkSystem(EPlayer.Player1);
        _testUtils = new TestUtils(_system);
    }

    [Test]
    public void HasMoneyForCity_BuyCity()
    {
        _testUtils.TransferSettlementResources(EPlayer.Player1);
        _system.BuildSettlement(0,0, EPlayer.Player1);
        _testUtils.TransferCityResources(EPlayer.Player1);
        
        _player.Play();
        
        Assert.That(_system.GetBoard().GetVertexStatus(0, 0), Is.EqualTo(EVertexStatus.City));
    }
}