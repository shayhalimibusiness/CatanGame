using CatanGame.Enums;

namespace CatanGame.Utils;

public static class GlobalResources
{
    public const int HorizontalsRoadsXSize = 5;
    public const int HorizontalsRoadsYSize = 4;
    public const int VerticalsRoadsXSize = 4;
    public const int VerticalsRoadsYSize = 5;
    public const int VerticesSize = 5;
    public const int TilesSize = 4;

    public static List<EResource> Resources = new List<EResource>
    {
        EResource.Iron,
        EResource.Sheep,
        EResource.Wheat,
        EResource.Tin,
        EResource.Wood
    };
}