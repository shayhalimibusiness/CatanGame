using CatanGame.Builder;
using CatanGame.Enums;
using CatanGame.Player;
using CatanGame.System;
using CatanGame.Utils;

namespace CatanGameTests.Player;

public class ParallelExpectimaxPlayer
{
    private IPlayer _player = null!;
    private ISystem _system = null!;
    private TestUtils _testUtils = null!;
    
    [SetUp]
    public void Setup()
    {
        var builder = BuilderFactory.Create1ParallelExpectimaxPlayerFullBuilder();
        _player = builder.Players.FirstOrDefault();
        _system = builder.System;
        _testUtils = new TestUtils(_system);
    }

    [Test]
    public void LooksForFirstMove_PlayOnDifferentSystems()
    {
        _player.PlayStartTurn();

        var settlements = 0;
        for (var i = 0; i < GlobalResources.VerticesSize; i++)
        {
            for (var j = 0; j < GlobalResources.VerticesSize; j++)
            {
                if (_system.GetBoard().GetVertexStatus(0, 0) == EVertexStatus.Settlement)
                {
                    settlements++;
                }
            }
        }
        Assert.That(settlements, Is.EqualTo(1));
    }
}