using CatanGame.Enums;

namespace CatanGame.System;

public class Vertex : IVertex
{
    public Vertex(EPlayer ePlayer, EVertexStatus eVertexStatus)
    {
        Owner = ePlayer;
        Status = eVertexStatus;
        GetPort = EResource.None;
    }
    public EPlayer Owner { get; set; }
    public EVertexStatus Status { get; set; }
    public EResource GetPort { get; set; }
}