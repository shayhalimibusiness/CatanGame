using CatanGame.Enums;

namespace CatanGame.System;

public interface IVertex
{
    void BuildSettlement();
    void BuildCity();
    EPlayer GetOwner();
    EVertexStatus GetStatus();
    bool IsPort();
}