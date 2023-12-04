using CatanGame.Enums;
using CatanGame.System;
using CatanGame.System.Board;
using CatanGame.System.Cards;
using CatanGame.Utils;

namespace CatanGame.Evaluator;

public class ParallelEvaluator : IEvaluator
{
    private readonly EPlayer _ePlayer = EPlayer.Player1;

    public decimal Evaluate()
    {
        throw new NotImplementedException();
    }

    public decimal EvaluateSystem(ISystem system)
    {
        decimal evaluation = 0;

        evaluation += EvaluateTotalPoints(system);
        evaluation += EvaluateResources(system);
        evaluation += EvaluateRoads(system);
        evaluation += EvaluateSettlements(system);
        evaluation += EvaluateCities(system);
        evaluation += EvaluateCards(system);

        return evaluation;
    }

    private decimal EvaluateTotalPoints(ISystem system)
    {
        var cards = system.GetCards(_ePlayer);
        return cards.GetTotalPoints();
    }

    private decimal EvaluateResources(ISystem system)
    {
        var cards = system.GetCards(_ePlayer);

        var evaluation = 
            Math.Max(EvaluateResourcesForAUse(GlobalResources.RoadResources, cards), 
            Math.Max(EvaluateResourcesForAUse(GlobalResources.SettlementResources, cards), 
            Math.Max(EvaluateResourcesForAUse(GlobalResources.CityResources, cards), 
                     EvaluateResourcesForAUse(GlobalResources.CardResources, cards))));

        evaluation = evaluation / 26;

        return evaluation;
    }

    private decimal EvaluateResourcesForAUse(Dictionary<EResource, int> need, ICards cards)
    {
        decimal evaluation = 0;
        
        var have = 0;
        foreach (var (eResource, amount) in need)
        {
            have += Math.Min(amount, cards.GetResources()[eResource]);
        }

        evaluation += (decimal)Math.Pow(have, 2) / 30;
        
        return evaluation;
    }

    private decimal EvaluateRoads(ISystem system)
    {
        var board = system.GetBoard();
        
        decimal evaluation = 0;
        
        foreach (ERoads eRoad in Enum.GetValues(typeof(ERoads)))
        {
            var roadsXSize = GlobalResources.GetRoadsSize(0, eRoad);
            var roadsYSize = GlobalResources.GetRoadsSize(1, eRoad);
            for (var i = 0; i < roadsXSize; i++)
            {
                for (var j = 0; j < roadsYSize; j++)
                {
                    if (board.GetRoadOwner(i, j, eRoad) == _ePlayer)
                    {
                        evaluation += 0.2m;
                        var buildLocation = board.CanBuildWithRoad(eRoad, i, j);
                        if (buildLocation.Item1 != -1)
                        {
                            evaluation += 0.2m;
                            evaluation += EvaluateVertexWealth(i, j, board) / 2;
                            evaluation += EvaluateVertexWealth(buildLocation.Item1, buildLocation.Item2, board) / 2;
                        }
                    }
                }
            }
        }

        return evaluation;
    }

    private decimal EvaluateSettlements(ISystem system)
    {
        var board = system.GetBoard();

        decimal evaluation = 0;

        for (var i = 0; i < GlobalResources.VerticesSize; i++)
        {
            for (var j = 0; j < GlobalResources.VerticesSize; j++)
            {
                if (board.GetVertexOwner(i, j) != _ePlayer ||
                    board.GetVertexStatus(i, j) != EVertexStatus.Settlement)
                {
                    continue;
                }

                evaluation += EvaluateVertexWealth(i, j, board);
                if (IsUsefulPort(i, j, system))
                {
                    evaluation += 0.08m;
                }
            }
        }

        return evaluation;
    }
    
    private decimal EvaluateCities(ISystem system)
    {
        var board = system.GetBoard();

        decimal evaluation = 0;

        for (var i = 0; i < GlobalResources.VerticesSize; i++)
        {
            for (var j = 0; j < GlobalResources.VerticesSize; j++)
            {
                if (board.GetVertexOwner(i, j) != _ePlayer ||
                    board.GetVertexStatus(i, j) != EVertexStatus.City)
                {
                    continue;
                }

                evaluation += 2 * EvaluateVertexWealth(i, j, board);
                if (IsUsefulPort(i, j, system))
                {
                    evaluation += 0.08m;
                }
            }
        }

        return evaluation;
    }

    private decimal EvaluateCards(ISystem system)
    {
        decimal evaluation = 0;

        evaluation += 0.35m * system.GetCardsBoughtByPlayer(_ePlayer);

        return evaluation;
    }

    private decimal EvaluateVertexWealth(int x, int y, IBoard board)
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
            if (board.GetTileResource(i, j) != EResource.None)
            {
                vertexProduction += 6 - Math.Abs(board.GetTileNumber(i, j) - 7);
            }
        }

        return vertexProduction / 36;
    }
    
    private bool IsTileInRange(int x, int y)
    {
        return x >= 0 && x < GlobalResources.TilesSize && y >= 0 && y < GlobalResources.TilesSize;
    }

    private bool IsUsefulPort(int i, int j, ISystem system)
    {
        var board = system.GetBoard();
        var cards = system.GetCards(_ePlayer);
        
        var port = board.PlayerHasPortIn(_ePlayer, i, j);
        return (port != EResource.None) && (!cards.HasPort(port));
    }
}