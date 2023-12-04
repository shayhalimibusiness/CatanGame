using CatanGame.Enums;
using CatanGame.System;
using CatanGame.System.Board;
using CatanGame.System.Cards;
using CatanGame.Utils;

namespace CatanGame.Evaluator;

public class Evaluator : IEvaluator
{
    private readonly EPlayer _ePlayer;

    private ISystem _system;
    private ICards _cards;
    private IBoard _board;
    
    public Evaluator(ISystem system, EPlayer ePlayer)
    {
        _ePlayer = ePlayer;
        
        _system = system;
        _cards = system.GetCards(ePlayer);
        _board = system.GetBoard();
    }
    
    public decimal EvaluateSystem(ISystem system)
    {
        var originalSystem = _system;
        _system = system;
        _board = _system.GetBoard();
        _cards = _system.GetCards(_ePlayer);
        var result = Evaluate();
        _system = originalSystem;
        _board = _system.GetBoard();
        _cards = _system.GetCards(_ePlayer);
        return result;
    }

    public decimal Evaluate()
    {
        decimal evaluation = 0;

        evaluation += EvaluateTotalPoints();
        evaluation += EvaluateResources();
        evaluation += EvaluateRoads();
        evaluation += EvaluateSettlements();
        evaluation += EvaluateCities();
        evaluation += EvaluateCards();

        return evaluation;
    }

    private decimal EvaluateTotalPoints()
    {
        return _cards.GetTotalPoints();
    }

    private decimal EvaluateResources()
    {
        var evaluation = 
            Math.Max(EvaluateResourcesForAUse(GlobalResources.RoadResources), 
            Math.Max(EvaluateResourcesForAUse(GlobalResources.SettlementResources), 
            Math.Max(EvaluateResourcesForAUse(GlobalResources.CityResources), 
                     EvaluateResourcesForAUse(GlobalResources.CardResources))));

        return evaluation;
    }

    private decimal EvaluateResourcesForAUse(Dictionary<EResource, int> need)
    {
        decimal evaluation = 0;
        
        var have = 0;
        foreach (var (eResource, amount) in need)
        {
            have += Math.Min(amount, _cards.GetResources()[eResource]);
        }

        evaluation += (decimal)Math.Pow(have, 2) / 30;
        
        return evaluation;
    }

    private decimal EvaluateRoads()
    {
        decimal evaluation = 0;
        
        foreach (ERoads eRoad in Enum.GetValues(typeof(ERoads)))
        {
            var roadsXSize = GlobalResources.GetRoadsSize(0, eRoad);
            var roadsYSize = GlobalResources.GetRoadsSize(1, eRoad);
            for (var i = 0; i < roadsXSize; i++)
            {
                for (var j = 0; j < roadsYSize; j++)
                {
                    if (_board.GetRoadOwner(i, j, eRoad) == _ePlayer)
                    {
                        evaluation += 0.2m;
                        var buildLocation = _board.CanBuildWithRoad(eRoad, i, j);
                        if (buildLocation.Item1 != -1)
                        {
                            evaluation += 0.2m;
                            evaluation += EvaluateVertexWealth(buildLocation.Item1, buildLocation.Item2) / 2;
                        }
                    }
                }
            }
        }

        return evaluation;
    }

    private decimal EvaluateSettlements()
    {
        decimal evaluation = 0;

        for (var i = 0; i < GlobalResources.VerticesSize; i++)
        {
            for (var j = 0; j < GlobalResources.VerticesSize; j++)
            {
                if (_board.GetVertexOwner(i, j) != _ePlayer ||
                    _board.GetVertexStatus(i, j) != EVertexStatus.Settlement)
                {
                    continue;
                }

                evaluation += EvaluateVertexWealth(i, j);
                if (IsUsefulPort(i, j))
                {
                    evaluation += 0.08m;
                }
            }
        }

        return evaluation;
    }
    
    private decimal EvaluateCities()
    {
        decimal evaluation = 0;

        for (var i = 0; i < GlobalResources.VerticesSize; i++)
        {
            for (var j = 0; j < GlobalResources.VerticesSize; j++)
            {
                if (_board.GetVertexOwner(i, j) != _ePlayer ||
                    _board.GetVertexStatus(i, j) != EVertexStatus.City)
                {
                    continue;
                }

                evaluation += 2 * EvaluateVertexWealth(i, j);
                if (IsUsefulPort(i, j))
                {
                    evaluation += 0.08m;
                }
            }
        }

        return evaluation;
    }

    private decimal EvaluateCards()
    {
        decimal evaluation = 0;

        evaluation += 0.35m * _system.GetCardsBoughtByPlayer(_ePlayer);

        return evaluation;
    }

    private decimal EvaluateVertexWealth(int x, int y)
    {
        var neighborTilesOffset = new[] { (0, 0), (0, -1), (-1, 0), (-1, -1) };
        var vertexProduction = 0m;
        foreach (var offset in neighborTilesOffset)
        {
            var (i, j) = offset;
            i += x;
            j += y;
            if (!IsTileInRange(i, j))
            {
                continue;
            }

            var tileNumber = _board.GetTileNumber(i, j);
            if (_board.GetTileResource(i, j) != EResource.None && tileNumber != 7)
            {
                vertexProduction += 6 - Math.Abs(tileNumber - 7);
            }
        }

        return vertexProduction / 36;
    }
    
    private bool IsTileInRange(int x, int y)
    {
        return x >= 0 && x < GlobalResources.TilesSize && y >= 0 && y < GlobalResources.TilesSize;
    }

    private bool IsUsefulPort(int i, int j)
    {
        var port = _board.PlayerHasPortIn(_ePlayer, i, j);
        return (port != EResource.None) && (!_cards.HasPort(port));
    }
}