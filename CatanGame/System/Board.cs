using System.Diagnostics;
using CatanGame.Enums;

namespace CatanGame.System;

public class Board : IBoard
{
    private IVertex[,] _vertices;
    private IRoad[,] _horizontalRoads;
    private IRoad[,] _verticalRoads;
    private EResource[,] _tiles;
    
    public Board(IVertex[,] vertices, IRoad[,] horizontalRoads, IRoad[,] verticalRoads, EResource[,] tiles)
    {
        _vertices = vertices;
        _horizontalRoads = horizontalRoads;
        _verticalRoads = verticalRoads;
        _tiles = tiles;
    }

    public EPlayer GetVertexOwner(int x, int y)
    {
        return _vertices[x, y].Owner;
    }

    public EVertexStatus GetVertexStatus(int x, int y)
    {
        return _vertices[x, y].Status;
    }

    public EPlayer GetRoadOwner(int x, int y, ERoads eRoads)
    {
        return eRoads switch
        {
            ERoads.Horizontals => _horizontalRoads[x, y].Owner,
            ERoads.Verticals => _verticalRoads[x, y].Owner,
            _ => throw new ArgumentOutOfRangeException(nameof(eRoads), eRoads, null)
        };
    }

    public EResource GetTileResource(int x, int y)
    {
        return _tiles[x, y];
    }

    public void SetVertexOwner(int x, int y, EPlayer ePlayer)
    {
        _vertices[x, y].Owner = ePlayer;
    }

    public void SetVertexStatus(int x, int y, EVertexStatus eVertexStatus)
    {
        _vertices[x, y].Status = eVertexStatus;
    }

    public void SetRoadOwner(int x, int y, ERoads eRoads, EPlayer ePlayer)
    {
        var matrix = eRoads switch
        {
            ERoads.Horizontals => _horizontalRoads,
            ERoads.Verticals => _verticalRoads
        };
        matrix[x, y].Owner = ePlayer;
    }

    public void SetTileResource(int x, int y, EResource resource)
    {
        _tiles[x, y] = resource;
    }
}