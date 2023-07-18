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
        if (_resources[eResource] + amount < 0)
        {
            throw new Exception("Can't be with negative amount of resources!");
        }
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
        if (_totalPoints + amount < 0)
        {
            throw new Exception("Can't be with a negative amount of points!");
        }
        _totalPoints += amount;
    }
}