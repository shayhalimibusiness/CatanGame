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
    
    public static Dictionary<EResource, int> RoadResources = new Dictionary<EResource, int>()
    {
        {EResource.Tin, 1},
        {EResource.Wood, 1},
    };
    
    public static Dictionary<EResource, int> SettlementResources = new Dictionary<EResource, int>()
    {
        {EResource.Sheep, 1},
        {EResource.Wheat, 1},
        {EResource.Wood, 1},
        {EResource.Tin, 1},
    };
    
    public static Dictionary<EResource, int> CityResources = new Dictionary<EResource, int>()
    {
        {EResource.Iron, 3},
        {EResource.Wheat, 2},
    };
    
    public static Dictionary<EResource, int> CardResources = new Dictionary<EResource, int>()
    {
        {EResource.Iron, 1},
        {EResource.Wheat, 1},
        {EResource.Sheep, 1},
    };
}