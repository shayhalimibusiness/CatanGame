using CatanGame.Enums;
using CatanGame.Models;
using CatanGame.System;

namespace CatanGame.Utils;

public class Mapper
{
    private const int VerticesBoardSize = 5;
    public static ShowBoardApi ShowBoardApiMapper(IBoard board)
    {
        var showBoardApi = new ShowBoardApi
        {
            Pixels = new Pixel[9, 9]
        };
        for (var i = 0; i < VerticesBoardSize; i++)
        {
            for (var j = 0; j < VerticesBoardSize; j++)
            {
                var eVertexStatus = board.GetVertexStatus(i, j);
                var eOwner = board.GetVertexOwner(i, j);
                showBoardApi.Pixels[i * 2, j * 2] = GetVertexRepresentation(eVertexStatus, eOwner);
                
                if (i + 1 < VerticesBoardSize)
                {
                    var verticalRoadOwner = board.GetRoadOwner(i + 1, j, ERoads.Verticals);
                    showBoardApi.Pixels[i + 1, j] = GetRoadRepresentation(ERoads.Verticals, verticalRoadOwner);
                }

                if (j + 1 < VerticesBoardSize)
                {
                    var horizontalRoadOwner = board.GetRoadOwner(i, j + 1, ERoads.Horizontals);
                    showBoardApi.Pixels[i, j + 1] = GetRoadRepresentation(ERoads.Horizontals, horizontalRoadOwner);
                }
            }
        }

        return showBoardApi;
    }

    private ShowBoardApi SetVertices(IBoard board, ShowBoardApi showBoardApi)
    {
        for (var i = 0; i < VerticesBoardSize; i++)
        {
            for (var j = 0; j < VerticesBoardSize; j++)
            {
                var eVertexStatus = board.GetVertexStatus(i, j);
                var eOwner = board.GetVertexOwner(i, j);
                showBoardApi.Pixels[i * 2, j * 2] = GetVertexRepresentation(eVertexStatus, eOwner);
                
                if (i + 1 < VerticesBoardSize)
                {
                    var verticalRoadOwner = board.GetRoadOwner(i + 1, j, ERoads.Verticals);
                    showBoardApi.Pixels[i + 1, j] = GetRoadRepresentation(ERoads.Verticals, verticalRoadOwner);
                }

                if (j + 1 < VerticesBoardSize)
                {
                    var horizontalRoadOwner = board.GetRoadOwner(i, j + 1, ERoads.Horizontals);
                    showBoardApi.Pixels[i, j + 1] = GetRoadRepresentation(ERoads.Horizontals, horizontalRoadOwner);
                }
            }
        }

        return showBoardApi;
    }

    private static Pixel GetVertexRepresentation(EVertexStatus vertexStatus, EPlayer owner)
    {
        var pixel = new Pixel();
        pixel.Sign = vertexStatus switch
        {
            EVertexStatus.Unsettled => 'U',
            EVertexStatus.Settlement => 'S',
            EVertexStatus.City => 'C',
            _ => pixel.Sign
        };
        pixel.Color = GetPlayerColor(owner);
        return pixel;
    }

    private static Pixel GetRoadRepresentation(ERoads eRoads, EPlayer owner)
    {
        var pixel = new Pixel
        {
            Sign = eRoads switch
            {
                ERoads.Horizontals => '-',
                ERoads.Verticals => '|',
                _ => throw new ArgumentOutOfRangeException(nameof(eRoads), eRoads, null)
            },
            Color = GetPlayerColor(owner)
        };
        return pixel;
    }

    private static ConsoleColor GetPlayerColor(EPlayer ePlayer)
    {
        return ePlayer switch
        {
            EPlayer.None => ConsoleColor.Gray,
            EPlayer.Player1 => ConsoleColor.Blue,
            EPlayer.Player2 => ConsoleColor.Red,
            EPlayer.Player3 => ConsoleColor.Green,
            EPlayer.Player4 => ConsoleColor.Yellow,
            _ => throw new ArgumentOutOfRangeException(nameof(ePlayer), ePlayer, null)
        };
    }
}