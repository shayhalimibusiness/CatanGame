using CatanGame.Enums;
using CatanGame.Models;
using CatanGame.System;
using CatanGame.System.Board;

namespace CatanGame.Utils;

public static class Mapper
{
    private const int VerticesBoardSize = 5;
    public static ShowBoardApi ShowBoardApiMapper(IBoard board)
    {
        var showBoardApi = new ShowBoardApi
        {
            Pixels = new Pixel[27, 9]
        };
        for (var i = 0; i < VerticesBoardSize; i++)
        {
            for (var j = 0; j < VerticesBoardSize; j++)
            {
                var eVertexStatus = board.GetVertexStatus(i, j);
                var eOwner = board.GetVertexOwner(i, j);
                var port = board.GetVertexPort(i, j);
                var pixels = GetVertexRepresentation(eVertexStatus, eOwner, port);
                for (var k = 0; k < 3; k++)
                {
                    if (pixels.Sign == null)
                    {
                        throw new Exception();
                    }
                    var pixel = new Pixel
                    {
                        Color = pixels.Color,
                        Sign = pixels.Sign?[k]
                    };
                    showBoardApi.Pixels[i*2*3 + k, j * 2] = pixel;
                }
                
                
                if (i + 1 < VerticesBoardSize)
                {
                    var verticalRoadOwner = board.GetRoadOwner(i, j, ERoads.Verticals);
                    pixels = GetRoadRepresentation(ERoads.Verticals, verticalRoadOwner);
                    for (var k = 0; k < 3; k++)
                    {
                        var pixel = new Pixel
                        {
                            Color = pixels.Color,
                            Sign = pixels.Sign?[k]
                        };
                        showBoardApi.Pixels[i*2*3 + 3 + k, j * 2] = pixel;
                    }
                }

                if (j + 1 < VerticesBoardSize)
                {
                    var verticalRoadOwner = board.GetRoadOwner(i, j, ERoads.Horizontals);
                    pixels = GetRoadRepresentation(ERoads.Horizontals, verticalRoadOwner);
                    for (var k = 0; k < 3; k++)
                    {
                        var pixel = new Pixel
                        {
                            Color = pixels.Color,
                            Sign = pixels.Sign?[k]
                        };
                        showBoardApi.Pixels[i*2*3 + k, j * 2 + 1] = pixel;
                    }
                }

                if (i + 1 < VerticesBoardSize && j + 1 < VerticesBoardSize)
                {
                    var resource = board.GetTileResource(i, j);
                    var number = board.GetTileNumber(i, j);
                    pixels = GetTileRepresentation(resource, number);
                    for (var k = 0; k < 3; k++)
                    {
                        var pixel = new Pixel
                        {
                            Color = pixels.Color,
                            Sign = pixels.Sign?[k]
                        };
                        showBoardApi.Pixels[i*2*3 + 3 + k, j * 2 + 1] = pixel;
                    }
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

    private static Pixels GetVertexRepresentation(EVertexStatus vertexStatus, EPlayer owner, EResource port)
    {
        var pixel = new Pixels();
        var portRepresentation = port switch
        {
            EResource.Iron => "--[Iron]--",
            EResource.Wood => "--[Wood]--",
            EResource.Wheat => "--[Wheat]-",
            EResource.Sheep => "--[Sheep]-",
            EResource.Tin => "--[Tin]---",
            EResource.PointCard => "--[All]---",
            EResource.None => "----------",
        };
        pixel.Sign = vertexStatus switch
        {
            EVertexStatus.Unsettled => new []
            {
                portRepresentation,
                "|  Empty |",
                "----------"
            },
            EVertexStatus.Settlement => new []
            {
                portRepresentation,
                "|Village|",
                "-------- "
            },
            EVertexStatus.City => new []
            {
                portRepresentation,
                "| City  |",
                "-------- "
            },
            _ => pixel.Sign
        };
        pixel.Color = GetPlayerColor(owner);
        return pixel;
    }

    private static Pixels GetRoadRepresentation(ERoads eRoads, EPlayer owner)
    {
        var pixel = new Pixels
        {
            Sign = eRoads switch
            {
                ERoads.Horizontals => new []
                {
                    "|     |",
                    "=======",
                    "|     |"
                },
                ERoads.Verticals => new []
                {
                    "   |||   ",
                    "   |||   ",
                    "   |||   "
                },
                _ => throw new ArgumentOutOfRangeException(nameof(eRoads), eRoads, null)
            },
            Color = GetPlayerColor(owner)
        };
        return pixel;
    }

    private static Pixels GetTileRepresentation(EResource eResource, int number)
    {
        var pixel = new Pixels
        {
            Sign = eResource switch
            {
                EResource.Iron => new []
                {
                    "-------- ", 
                    $"|Iron({number:D2})",
                    "-------- "
                },
                EResource.Wheat =>new []
                {
                    "-------- ", 
                    $"Wheat({number:D2})",
                    "-------- "
                },
                EResource.Sheep => new []
                {
                    "-------- ", 
                    $"Sheep({number:D2})",
                    "-------- "
                },
                EResource.Tin => new []
                {
                    "-------- ", 
                    $"|Tin({number:D2})|",
                    "-------- "
                },
                EResource.Wood => new []
                {
                    "-------- ", 
                    $"|Wood({number:D2})",
                    "-------- "
                },
                EResource.None => new []
                {
                    "--------", 
                    "|Desert|",
                    "--------"
                },
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