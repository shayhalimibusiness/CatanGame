using CatanGame.Enums;
using CatanGame.Game;
using CatanGame.System;
using CatanGame.System.Board;
using CatanGame.System.Cards;
using CatanGame.Utils;

namespace CatanGame.Evaluator;

public class Evaluator : IEvaluator
{
    private readonly EPlayer _ePlayer;

    private readonly ISystem _system;
    private readonly ICards _cards;
    private readonly IBoard _board;
    
    public Evaluator(ISystem system, EPlayer ePlayer)
    {
        _ePlayer = ePlayer;
        
        _system = system;
        _cards = system.GetCards(ePlayer);
        _board = system.GetBoard();
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
        var evaluation = GlobalResources.Resources
            .Aggregate<EResource, decimal>(0, (current, eResource) => 
                current + (decimal)_cards.GetResources()[eResource] / 30);

        evaluation += EvaluateResourcesForAUse(GlobalResources.RoadResources);
        evaluation += EvaluateResourcesForAUse(GlobalResources.SettlementResources);
        evaluation += EvaluateResourcesForAUse(GlobalResources.CityResources);
        evaluation += EvaluateResourcesForAUse(GlobalResources.CardResources);

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
                if (_board.PlayerHasPortIn(_ePlayer, i, j) != EResource.None)
                {
                    evaluation += 0.25m;
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
                if (_board.PlayerHasPortIn(_ePlayer, i, j) != EResource.None)
                {
                    evaluation += 0.25m;
                }
            }
        }

        return evaluation;
    }

    private decimal EvaluateCards()
    {
        decimal evaluation = 0;

        evaluation += 0.23m * _system.GetCardsBoughtByPlayer(_ePlayer);

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
            if (_board.GetTileResource(i, j) != EResource.None)
            {
                vertexProduction += _board.GetTileNumber(i, j);
            }
        }

        return vertexProduction / 36;
    }
    
    private bool IsTileInRange(int x, int y)
    {
        return x >= 0 && x < GlobalResources.TilesSize && y >= 0 && y < GlobalResources.TilesSize;
    }
}