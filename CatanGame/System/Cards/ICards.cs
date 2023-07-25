using CatanGame.Enums;

namespace CatanGame.System;

public interface ICards
{
    void TransferResources(EResource eResource, int amount);
    Dictionary<EResource, int> GetResources();
    int GetTotalPoints();
    void TransferTotalPoints(int amount);
    public int Iron { get; set; }
    public int Wheat { get; set; }
    public int Sheep { get; set; }
    public int Wood{ get; set; }
    public int Tin { get; set; }
}