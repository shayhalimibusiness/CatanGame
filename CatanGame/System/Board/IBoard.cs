using CatanGame.Enums;

namespace CatanGame.System.Board;

public interface IBoard
{
    EPlayer GetVertexOwner(int x, int y);
    EVertexStatus GetVertexStatus(int x, int y);
    EPlayer GetRoadOwner(int x, int y, ERoads eRoads);
    EResource GetTileResource(int x, int y);
    int GetTileNumber(int x, int y);

    EResource PlayerHasPortIn(EPlayer ePlayer, int x, int y);
    Dictionary<EPlayer, Dictionary<EResource, int>> GetResources(int diceRoll);

    void SetVertexOwner(int x, int y, EPlayer ePlayer);
    void SetVertexStatus(int x, int y, EVertexStatus eVertexStatus);
    void SetRoadOwner(int x, int y, ERoads eRoads, EPlayer ePlayer);
    void SetTileResource(int x, int y, EResource resource);
    
}