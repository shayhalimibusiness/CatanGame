using CatanGame.Enums;
using CatanGame.System;
using CatanGame.System.Board;
using CatanGame.System.Cards;
using CatanGame.Utils;

namespace CatanGame.Evaluator;

public class Evaluator : IEvaluator
{
    private EPlayer _ePlayer;

    private readonly ICards _cards;
    private readonly IBoard _board;
    
    public Evaluator(ISystem system, EPlayer ePlayer)
    {
        _ePlayer = ePlayer;

        _cards = system.GetCards(ePlayer);
        _board = system.GetBoard();
    }

    public decimal Evaluate()
    {
        decimal evaluation = 0;

        evaluation += EvaluateTotalPoints();
        evaluation += EvaluateResources();
        evaluation += EvaluateSettlements();
        evaluation += EvaluateCities();

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
                current + (decimal)_cards.GetResources()[eResource] / 12);

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

        evaluation += (decimal)Math.Pow(have, 2) / 8;
        
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
                
                evaluation += 0.5m;
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
                
                evaluation += 1m;
                if (_board.PlayerHasPortIn(_ePlayer, i, j) != EResource.None)
                {
                    evaluation += 0.25m;
                }
            }
        }

        return evaluation;
    }
}