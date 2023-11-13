using CatanGame.Enums;

namespace CatanGame.System;

public class Road : IRoad
{
    public EPlayer Owner { get; set; } = EPlayer.None;

    public Road()
    {
    }

    public Road(IRoad other)
    {
        Owner = other.Owner;
    }
}