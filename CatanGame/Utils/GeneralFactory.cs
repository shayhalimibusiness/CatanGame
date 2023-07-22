using CatanGame.Enums;

namespace CatanGame.Utils;

public static class GeneralFactory
{
    public static Dictionary<ERoads, string> CreateRoadsNames()
    {
        var names = new Dictionary<ERoads, string>
        {
            { ERoads.Horizontals , "horizontal"},
            { ERoads.Verticals , "vertical"}
        };

        return names;
    }

    public static Dictionary<EPlayer, string> CreatePlayersNames()
    {
        var nameDictionary = new Dictionary<EPlayer, string>
        {
            { EPlayer.Player1, "Player1" },
            { EPlayer.Player2, "Player2" }
        };
        return nameDictionary;
    }

    public static Dictionary<EResource, string> CreateResourcesNames()
    {
        var resourcesNameDictionary = new Dictionary<EResource, string>
        {
            { EResource.Iron, "Iron" },
            { EResource.Sheep, "Sheep" },
            { EResource.Tin, "Tin" },
            { EResource.Wheat, "Wheat" },
            { EResource.Wood, "Wood" },
            { EResource.PointCard, "PointCards" }
        };

        return resourcesNameDictionary;
    }
}