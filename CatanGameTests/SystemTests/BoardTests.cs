using CatanGame.Enums;
using CatanGame.System;
using CatanGame.System.Board;

namespace CatanGameTests.SystemTests;

public class BoardTests
{
    private IBoard _board;
    
    [SetUp]
    public void Setup()
    {
        _board = BoardFactory.CreateBlankBoard();
    }

    [Test]
    public void GetVertexOwner_IsNone_GetNone()
    {
        var owner = _board.GetVertexOwner(0, 0);
        Assert.That(owner, Is.EqualTo(EPlayer.None));
    }
    
    [Test]
    public void GetVertexStatus_IsUnsettled_GetUnsettled()
    {
        var status = _board.GetVertexStatus(0, 0);
        Assert.That(status, Is.EqualTo(EVertexStatus.Unsettled));
    }
    
    [Test]
    public void GetRoadOwner_IsNone_GetNone()
    {
        var owner = _board.GetRoadOwner(0, 0, ERoads.Horizontals);
        Assert.That(owner, Is.EqualTo(EPlayer.None));
    }
    
    [Test]
    public void GetTileResource_IsNone_GetNone()
    {
        var resource = _board.GetTileResource(0, 0);
        Assert.That(resource, Is.EqualTo(EResource.None));
    }
    
    [Test]
    public void SetVertexOwner_SetPlayer1_GetPlayer1()
    {
        _board.SetVertexOwner(0,0, EPlayer.Player1);
        var owner = _board.GetVertexOwner(0, 0);
        Assert.That(owner, Is.EqualTo(EPlayer.Player1));
    }
    
    [Test]
    public void SetVertexStatus_SetSettlement_GetSettlement()
    {
        _board.SetVertexStatus(0,0,EVertexStatus.Settlement);
        var status = _board.GetVertexStatus(0, 0);
        Assert.That(status, Is.EqualTo(EVertexStatus.Settlement));
    }
    
    [Test]
    public void SetRoadOwner_SetPlayer1_GetPlayer1()
    {
        _board.SetRoadOwner(0, 0, ERoads.Horizontals, EPlayer.Player1);
        var owner = _board.GetRoadOwner(0, 0, ERoads.Horizontals);
        Assert.That(owner, Is.EqualTo(EPlayer.Player1));
    }
    
    [Test]
    public void SetTileResource_SetTin_GetTin()
    {
        _board.SetTileResource(0, 0, EResource.Tin);
        var resource = _board.GetTileResource(0, 0);
        Assert.That(resource, Is.EqualTo(EResource.Tin));
    }
    
    [Test]
    public void GetTileNumber_Is1_Get1()
    {
        var number = _board.GetTileNumber(0, 0);
        Assert.That(number, Is.EqualTo(1));
    }
    
    [Test]
    public void GetResources_Dice1_GetAllResources()
    {
        _board.SetTileResource(0, 0, EResource.Tin);
        _board.SetVertexOwner(0,0, EPlayer.Player1);
        _board.SetVertexStatus(0, 0, EVertexStatus.Settlement);
        _board.SetVertexOwner(0,1, EPlayer.Player1);
        _board.SetVertexStatus(0, 1, EVertexStatus.City);
        _board.SetVertexOwner(1,1, EPlayer.Player2);
        _board.SetVertexStatus(1, 1, EVertexStatus.Settlement);
        
        var resources = _board.GetResources(1);
        Assert.That(resources[EPlayer.Player1][EResource.Tin], Is.EqualTo(3));
        Assert.That(resources[EPlayer.Player2][EResource.Tin], Is.EqualTo(1));
    }
}