using CatanGame.Enums;

namespace CatanGame.System;

public static class BoardFactory
{
    public static IBoard CreateBlankBoard()
    {
        var tiles = CreateBlankTiles();
        var vertices = CreateBlankVertices();
        var horizontalRoads = CreateBlankHorizontalRoads();
        var verticalRoads = CreateBlankVerticalRoads();
        var board = new Board(
            vertices: vertices, 
            horizontalRoads: horizontalRoads, 
            verticalRoads: verticalRoads,
            tiles: tiles);

        return board;
    }

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
    
    private static IVertex[,] CreateBlankVertices()
    {
        var map = new IVertex[4, 5];
        for (var i = 0; i < map.GetLength(0); i++)
        {
            for (var j = 0; j < map.GetLength(1); j++)
            {
                map[i, j] = new Vertex(EPlayer.None, EVertexStatus.Unsettled);
            }
        }

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
    
    private static IRoad[,] CreateBlankHorizontalRoads()
    {
        var map = new IRoad[4, 5];
        for (var i = 0; i < map.GetLength(0); i++)
        {
            for (var j = 0; j < map.GetLength(1); j++)
            {
                map[i, j] = new Road();
            }
        }

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

    private static IRoad[,] CreateBlankVerticalRoads()
    {
        var map = new IRoad[4, 5];
        for (var i = 0; i < map.GetLength(0); i++)
        {
            for (var j = 0; j < map.GetLength(1); j++)
            {
                map[i, j] = new Road();
            }
        }

        return map;
    }

    private static Tile[,] CreateExampleTiles()
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

        var tiles = new Tile[4, 4];

        for (var i = 0; i < 4; i++)
        {
            for (var j = 0; j < 4; j++)
            {
                tiles[i, j] = new Tile
                {
                    EResource = map[i, j],
                    Number = 1,
                };
            }
        }

        return tiles;
    }

    private static Tile[,] CreateBlankTiles()
    {
        var map = new Tile[4, 4];
        for (var i = 0; i < map.GetLength(0); i++)
        {
            for (var j = 0; j < map.GetLength(1); j++)
            {
                map[i, j] = new Tile
                {
                    EResource = EResource.None,
                    Number = 1
                };
            }
        }

        return map;
    }
}