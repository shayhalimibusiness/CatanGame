using CatanGame.Enums;
using CatanGame.Models;
using CatanGame.System;

namespace CatanGame.Utils;

public static class Mapper
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
                    var verticalRoadOwner = board.GetRoadOwner(i, j, ERoads.Verticals);
                    showBoardApi.Pixels[i*2+1, j*2] = GetRoadRepresentation(ERoads.Verticals, verticalRoadOwner);
                }

                if (j + 1 < VerticesBoardSize)
                {
                    var horizontalRoadOwner = board.GetRoadOwner(i, j, ERoads.Horizontals);
                    showBoardApi.Pixels[i*2, j*2+1] = GetRoadRepresentation(ERoads.Horizontals, horizontalRoadOwner);
                }

                if (i + 1 < VerticesBoardSize && j + 1 < VerticesBoardSize)
                {
                    var resource = board.GetTileResource(i, j);
                    showBoardApi.Pixels[i*2+1, j*2+1] = GetTileRepresentation(resource);
                }
            }
        }

        return showBoardApi;
    }

    public static ShowStatusApi ShowStatusApiMapper(ISystem system)
    {
        var allCards = system.GetAllCards();
        var totalPoints = new Dictionary<EPlayer, int>();
        foreach (var (ePlayer, cards) in allCards)
        {
            totalPoints[ePlayer] = cards.GetTotalPoints();
        }
        
        var showStatusApi = new ShowStatusApi
        {
            TotalPoints = totalPoints
        };

        return showStatusApi;
    }

    public static ConsoleColor GetPlayerColor(EPlayer ePlayer)
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

    private static Pixel GetVertexRepresentation(EVertexStatus vertexStatus, EPlayer owner)
    {
        var pixel = new Pixel();
        pixel.Sign = vertexStatus switch
        {
            EVertexStatus.Unsettled => "U-",
            EVertexStatus.Settlement => "S-",
            EVertexStatus.City => "C-",
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
                ERoads.Horizontals => "--",
                ERoads.Verticals => "| ",
                _ => throw new ArgumentOutOfRangeException(nameof(eRoads), eRoads, null)
            },
            Color = GetPlayerColor(owner)
        };
        return pixel;
    }

    private static Pixel GetTileRepresentation(EResource eResource)
    {
        var pixel = new Pixel
        {
            Sign = eResource switch
            {
                EResource.Iron => "I ",
                EResource.Wheat => "H ",
                EResource.Sheep => "S ",
                EResource.Tin => "T ",
                EResource.Wood => "W ",
                EResource.None => "D ",
                _ => throw new ArgumentOutOfRangeException(nameof(eResource), eResource, null)
            },
            Color = eResource switch
            {
                EResource.Iron => ConsoleColor.Blue,
                EResource.Wheat => ConsoleColor.Yellow,
                EResource.Sheep => ConsoleColor.White,
                EResource.Tin => ConsoleColor.DarkRed,
                EResource.Wood => ConsoleColor.Green,
                EResource.None => ConsoleColor.Magenta,
                _ => throw new ArgumentOutOfRangeException(nameof(eResource), eResource, null)
            }
        };

        return pixel;
    }
}