using CatanGame.Enums;

namespace CatanGame.System;

public class BoardFactory
{
    public static IBoard CreateExampleBoard()
    {
        var vertices = CreateExampleVertices();
        var horizontalRoads = CreateExampleHorizontalRoads();
        var verticalRoads = CreateExampleVerticalRoads();
        var tiles = CreateExampleTiles();
        var board = new Board(vertices, horizontalRoads, verticalRoads, tiles);
        return board;
    }
    
    private static IVertex[,] CreateExampleVertices()
    {
        IVertex[,] map = {
            {
                new Vertex(EPlayer.Player1, EVertexStatus.City),
                new Vertex(EPlayer.Player2, EVertexStatus.Settlement),
                new Vertex(EPlayer.Player3, EVertexStatus.Unsettled),
                new Vertex(EPlayer.Player4, EVertexStatus.City),
                new Vertex(EPlayer.None, EVertexStatus.Settlement)
            },
            {
                new Vertex(EPlayer.Player2, EVertexStatus.Settlement),
                new Vertex(EPlayer.Player3, EVertexStatus.Unsettled),
                new Vertex(EPlayer.Player4, EVertexStatus.City),
                new Vertex(EPlayer.None, EVertexStatus.Settlement),
                new Vertex(EPlayer.Player1, EVertexStatus.City)
            },
            {
                new Vertex(EPlayer.Player3, EVertexStatus.Unsettled),
                new Vertex(EPlayer.Player4, EVertexStatus.City),
                new Vertex(EPlayer.None, EVertexStatus.Settlement),
                new Vertex(EPlayer.Player1, EVertexStatus.City),
                new Vertex(EPlayer.Player2, EVertexStatus.Settlement)
            },
            {
                new Vertex(EPlayer.Player4, EVertexStatus.City),
                new Vertex(EPlayer.None, EVertexStatus.Settlement),
                new Vertex(EPlayer.Player1, EVertexStatus.City),
                new Vertex(EPlayer.Player2, EVertexStatus.Settlement),
                new Vertex(EPlayer.Player3, EVertexStatus.Unsettled)
            },
            {
                new Vertex(EPlayer.None, EVertexStatus.Settlement),
                new Vertex(EPlayer.Player1, EVertexStatus.City),
                new Vertex(EPlayer.Player2, EVertexStatus.Settlement),
                new Vertex(EPlayer.Player3, EVertexStatus.Unsettled),
                new Vertex(EPlayer.Player4, EVertexStatus.City)
            }
        };

        return map;
    }
    
    private static IRoad[,] CreateExampleHorizontalRoads()
    {
        IRoad[,] map = {
            {
                new Road { Owner = EPlayer.Player1 },
                new Road { Owner = EPlayer.Player2 },
                new Road { Owner = EPlayer.Player3 },
                new Road { Owner = EPlayer.None }
            },
            {
                new Road { Owner = EPlayer.Player2 },
                new Road { Owner = EPlayer.Player3 },
                new Road { Owner = EPlayer.None },
                new Road { Owner = EPlayer.Player1 }
            },
            {
                new Road { Owner = EPlayer.Player3 },
                new Road { Owner = EPlayer.None },
                new Road { Owner = EPlayer.Player1 },
                new Road { Owner = EPlayer.Player2 }
            },
            {
                new Road { Owner = EPlayer.None },
                new Road { Owner = EPlayer.Player1 },
                new Road { Owner = EPlayer.Player2 },
                new Road { Owner = EPlayer.Player3 }
            },
            {
                new Road { Owner = EPlayer.Player1 },
                new Road { Owner = EPlayer.Player2 },
                new Road { Owner = EPlayer.Player3 },
                new Road { Owner = EPlayer.None }
            }
        };

        return map;
    }

    private static IRoad[,] CreateExampleVerticalRoads()
    {
        IRoad[,] map = {
            {
                new Road { Owner = EPlayer.Player1 },
                new Road { Owner = EPlayer.Player2 },
                new Road { Owner = EPlayer.Player3 },
                new Road { Owner = EPlayer.Player4 },
                new Road { Owner = EPlayer.None }
            },
            {
                new Road { Owner = EPlayer.None },
                new Road { Owner = EPlayer.Player1 },
                new Road { Owner = EPlayer.Player2 },
                new Road { Owner = EPlayer.Player3 },
                new Road { Owner = EPlayer.Player4 }
            },
            {
                new Road { Owner = EPlayer.Player4 },
                new Road { Owner = EPlayer.None },
                new Road { Owner = EPlayer.Player1 },
                new Road { Owner = EPlayer.Player2 },
                new Road { Owner = EPlayer.Player3 }
            },
            {
                new Road { Owner = EPlayer.Player3 },
                new Road { Owner = EPlayer.Player4 },
                new Road { Owner = EPlayer.None },
                new Road { Owner = EPlayer.Player1 },
                new Road { Owner = EPlayer.Player2 }
            }
        };

        return map;
    }

    private static EResource[,] CreateExampleTiles()
    {
        var map = new EResource[4, 4]
        {
            {
                EResource.Wheat,
                EResource.Iron,
                EResource.Sheep,
                EResource.Wood
            },
            {
                EResource.Tin,
                EResource.None,
                EResource.Wood,
                EResource.Wheat
            },
            {
                EResource.Sheep,
                EResource.Tin,
                EResource.Wheat,
                EResource.Iron
            },
            {
                EResource.None,
                EResource.Sheep,
                EResource.Iron,
                EResource.Wood
            }
        };

        return map;
    }
}