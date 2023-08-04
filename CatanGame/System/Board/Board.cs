using CatanGame.Enums;

namespace CatanGame.System.Board;

public class Board : IBoard
{
    private IVertex[,] _vertices;
    private IRoad[,] _horizontalRoads;
    private IRoad[,] _verticalRoads;
    private Tile[,] _tiles;

    public Board(IVertex[,] vertices, IRoad[,] horizontalRoads, IRoad[,] verticalRoads, Tile[,] tiles)
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
        return _tiles[x, y].EResource;
    }

    public int GetTileNumber(int x, int y)
    {
        return _tiles[x, y].Number;
    }

    public EResource PlayerHasPortIn(EPlayer ePlayer, int x, int y)
    {
        return _vertices[x, y].Owner != ePlayer ? 
            EResource.None : 
            _vertices[x, y].GetPort;
    }

    public EResource GetVertexPort(int x, int y)
    {
        return _vertices[x, y].GetPort;
    }

    public Dictionary<EPlayer, Dictionary<EResource, int>> GetResources(int diceRoll)
    {
        var resources = new Dictionary<EPlayer, Dictionary<EResource, int>>();
        
        for (var i = 0; i < _tiles.GetLength(0); i++)
        {
            for (var j = 0; j < _tiles.GetLength(1); j++)
            {
                var tile = _tiles[i, j];
                if (tile.EResource == EResource.None)
                {
                    continue;
                }
                GetResourcesAux(i, j, tile.EResource, resources);
                GetResourcesAux(i+1, j, tile.EResource, resources);
                GetResourcesAux(i, j+1, tile.EResource, resources);
                GetResourcesAux(i+1, j+1, tile.EResource, resources);
            }
        }

        return resources;
    }

    public void GetResourcesAux(int x, int y, EResource eResource, 
        Dictionary<EPlayer, Dictionary<EResource, int>> resources)
    {
        var vertex = _vertices[x, y];
        var owner = vertex.Owner;
        var status = vertex.Status;
        
        if (status == EVertexStatus.Unsettled)
        {
            return;
        }

        var multiplier = status == EVertexStatus.Settlement ? 1 : 2;

        if (!resources.ContainsKey(owner))
        {
            resources[owner] = new Dictionary<EResource, int>();
        }

        if (!resources[owner].ContainsKey(eResource))
        {
            resources[owner][eResource] = 0;
        }

        resources[owner][eResource] += multiplier;
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
            ERoads.Verticals => _verticalRoads,
            _ => throw new ArgumentOutOfRangeException(nameof(eRoads), eRoads, null)
        };
        matrix[x, y].Owner = ePlayer;
    }

    public void SetTileResource(int x, int y, EResource resource)
    {
        _tiles[x, y].EResource = resource;
    }
}