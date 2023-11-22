using CatanGame.Enums;
using CatanGame.System.Board;

namespace CatanGame.Utils;

public static class SystemExtensions
{
    public static (int, int) CanBuildWithRoad(this IBoard board, ERoads eRoads, int x, int y)
    {
        switch (eRoads)
        {
            case ERoads.Horizontals:
                return board.IsVertexBuildable(x, y) ? (x, y) 
                    : board.IsVertexBuildable(x, y + 1) ? (x, y+1) : (-1, -1);
            case ERoads.Verticals:
                return board.IsVertexBuildable(x, y) ? (x, y) 
                    : board.IsVertexBuildable(x + 1, y) ? (x+1, y) : (-1, -1);
            default:
                throw new ArgumentOutOfRangeException(nameof(eRoads), eRoads, null);
        }
    }
    
    private static bool IsVertexBuildable(this IBoard board, int x, int y)
    {
         var neighborVertices = GetNeighborVerticesToVertex(x, y);
        var isFarFromSettlements = !(
            from neighbor in neighborVertices
            let nX = neighbor.Item1
            let nY = neighbor.Item2
            where board.GetVertexOwner(nX, nY) != EPlayer.None 
            select nX
        ).Any();

        return isFarFromSettlements;
    }
    
    private static List<(int, int)> GetNeighborVerticesToVertex(int x, int y)
    {
        var initialList = new List<(int, int)>
        {
            (x, y-1),
            (x, y+1),
            (x-1, y),
            (x+1, y),
        };

        return initialList.Where(pair => IsVertexInRange(pair.Item1, pair.Item2)).ToList();
    }
    
    private static bool IsVertexInRange(int x, int y)
    {
        return x is >= 0 and < GlobalResources.VerticesSize && y is >= 0 and < GlobalResources.VerticesSize;
    }
}