using CatanGame.Enums;

namespace CatanGame.System;

public interface IVertex
{
    EPlayer Owner { get; set; }
    EVertexStatus Status { get; set; }
    bool IsPort { get; set; }
}