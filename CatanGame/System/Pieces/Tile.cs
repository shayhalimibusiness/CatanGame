using CatanGame.Enums;

namespace CatanGame.System;

public class Tile
{
    public EResource EResource { get; set; }
    public int Number { get; set; }

    public Tile()
    {
    }

    public Tile(Tile other)
    {
        EResource = other.EResource;
        Number = other.Number;
    }
}