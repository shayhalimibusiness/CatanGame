using CatanGame.Enums;

namespace CatanGame.System;

public class Vertex : IVertex
{
    public Vertex(EPlayer ePlayer, EVertexStatus eVertexStatus, EResource port = EResource.None)
    {
        Owner = ePlayer;
        Status = eVertexStatus;
        GetPort = port;
    }
    public EPlayer Owner { get; set; }
    public EVertexStatus Status { get; set; }
    public EResource GetPort { get; set; }
}