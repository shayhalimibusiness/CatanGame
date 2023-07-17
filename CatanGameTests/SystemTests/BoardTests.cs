using CatanGame.Enums;
using CatanGame.System;

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
}