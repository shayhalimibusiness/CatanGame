using CatanGame.Enums;

namespace CatanGame.System;

public interface IBoard
{
    EPlayer GetVertexOwner(int x, int y);
    EVertexStatus GetVertexStatus(int x, int y);
    EPlayer GetRoadOwner(int x, int y, ERoads eRoads);
    EResource GetTileResource(int x, int y);

    void SetVertexOwner(int x, int y, EPlayer ePlayer);
    void SetVertexStatus(int x, int y, EVertexStatus eVertexStatus);
    void SetRoadOwner(int x, int y, ERoads eRoads);
    void SetTileResource(int x, int y, EResource resource);

}