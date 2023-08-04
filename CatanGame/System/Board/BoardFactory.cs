using CatanGame.Enums;

namespace CatanGame.System.Board;

public static class BoardFactory
{
    public static IBoard CreateRandomBoard()
    {
        var vertices = CreateBlankVerticesWithPort();
        var horizontalRoads = CreateBlankHorizontalRoads();
        var verticalRoads = CreateBlankVerticalRoads();

        var random = new Random();
        var tiles = new Tile[4, 4];
        for (var i = 0; i < tiles.GetLength(0); i++)
        {
            for (var j = 0; j < tiles.GetLength(1); j++)
            {
                tiles[i, j] = new Tile
                {
                    Number = random.Next(2, 13),
                    EResource = random.Next(1, 7) switch
                    {
                        1 => EResource.Iron,
                        2 => EResource.None,
                        3 => EResource.Sheep,
                        4 => EResource.Tin,
                        5 => EResource.Wheat,
                        6 => EResource.Wood,
                        _ => throw new ArgumentOutOfRangeException()
                    }
                };
            }
        }

        var board = new Board(
            vertices: vertices, 
            horizontalRoads: horizontalRoads, 
            verticalRoads: verticalRoads,
            tiles: tiles);

        return board; 
    }
    
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
        var map = new IVertex[5, 5];
        for (var i = 0; i < map.GetLength(0); i++)
        {
            for (var j = 0; j < map.GetLength(1); j++)
            {
                map[i, j] = new Vertex(EPlayer.None, EVertexStatus.Unsettled);
            }
        }

        return map;
    }
    
    private static IVertex[,] CreateBlankVerticesWithPort()
    {
        var map = new IVertex[5, 5];
        for (var i = 0; i < map.GetLength(0); i++)
        {
            for (var j = 0; j < map.GetLength(1); j++)
            {
                var random = new Random();
                var port = random.Next(0, 4) == 0 ? (EResource)random.Next(0, 6) : EResource.None; 
                map[i, j] = new Vertex(
                    EPlayer.None, 
                    EVertexStatus.Unsettled,
                    port
                    );
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
        var map = new IRoad[5, 4];
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