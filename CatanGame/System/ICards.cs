using CatanGame.Enums;

namespace CatanGame.System;

public interface ICards
{
    Dictionary<EResource, int> GetResources();
    int GetTotalPoints();
}