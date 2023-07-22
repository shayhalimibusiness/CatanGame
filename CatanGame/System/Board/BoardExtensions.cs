using CatanGame.Enums;

namespace CatanGame.System.Board;

public static class BoardExtensions
{
    public static bool PlayerOwnPort(this IBoard board, EPlayer ePlayer, EResource eResource)
    {
        const int verticesSize = 5;
        for (var i = 0; i < verticesSize; i++)
        {
            for (var j = 0; j < verticesSize; j++)
            {
                if (board.PlayerHasPortIn(ePlayer, i, j) == eResource)
                {
                    return true;
                }
            }
        }

        return false;
    }
}