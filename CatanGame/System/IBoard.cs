using CatanGame.Enums;

namespace CatanGame.System;

public interface IBoard
{
    EPlayer GetVertexOwner(int x, int y);
    EVertexStatus GetVertexStatus(int x, int y);
    EPlayer GetRoadOwner(int x, int y, ERoads eRoads);

}