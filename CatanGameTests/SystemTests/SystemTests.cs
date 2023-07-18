using CatanGame.Enums;
using CatanGame.System;

namespace CatanGameTests.SystemTests;

public class SystemTests
{
    private ISystem _system;
    
    [SetUp]
    public void Setup()
    {
        _system = SystemFactory.CreateSystem();
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
        cards.TransferResources(EResource.Sheep, 1);
        cards.TransferResources(EResource.Wood, 1);
        cards.TransferResources(EResource.Wheat, 1);
        cards.TransferResources(EResource.Tin, 1);
        
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
}