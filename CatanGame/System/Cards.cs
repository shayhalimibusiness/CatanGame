using CatanGame.Enums;

namespace CatanGame.System;

public class Cards : ICards
{
    private readonly Dictionary<EResource, int> _resources;
    private int _totalPoints;

    public Cards()
    {
        _resources = new Dictionary<EResource, int>();
        _totalPoints = 0;
    }

    public void TransferResources(EResource eResource, int amount)
    {
        _resources.TryAdd(eResource, 0);
        _resources[eResource] += amount;
    }

    public Dictionary<EResource, int> GetResources()
    {
        return _resources;
    }

    public int GetTotalPoints()
    {
        return _totalPoints;
    }

    public void TransferTotalPoints(int amount)
    {
        _totalPoints += amount;
    }
}