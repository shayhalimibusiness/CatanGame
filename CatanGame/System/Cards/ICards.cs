using CatanGame.Enums;

namespace CatanGame.System;

public interface ICards
{
    void TransferResources(EResource eResource, int amount);
    Dictionary<EResource, int> GetResources();
    int GetTotalPoints();
    void TransferTotalPoints(int amount);
}