using CatanGame.Enums;

namespace CatanGame.System.Cards;

public interface ICards
{
    Dictionary<EResource, int> GetResources();
    int GetTotalPoints();
    
    bool HasPort(EResource eResource);
    void AddPort(EResource eResource);
    void RemovePort(EResource eResource);
    
    public int Iron { get; set; }
    public int Wheat { get; set; }
    public int Sheep { get; set; }
    public int Wood{ get; set; }
    public int Tin { get; set; }
    
    void TransferTotalPoints(int amount);
    void TransferResources(EResource eResource, int amount);
}