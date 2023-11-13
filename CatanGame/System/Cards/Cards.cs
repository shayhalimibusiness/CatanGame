using CatanGame.Enums;

namespace CatanGame.System.Cards;

public class Cards : ICards
{
    private readonly Dictionary<EResource, int> _resources;
    private int _totalPoints;
    private readonly Dictionary<EResource, int> _ports;

    public Cards()
    {
        _resources = new Dictionary<EResource, int>
        {
            { EResource.Iron , 0},
            { EResource.Sheep , 0},
            { EResource.Wood , 0},
            { EResource.Wheat , 0},
            { EResource.Tin , 0},
        };
        _totalPoints = 0;
        _ports = new Dictionary<EResource, int>
        {
            { EResource.Iron , 0},
            { EResource.Sheep , 0},
            { EResource.Wood , 0},
            { EResource.Wheat , 0},
            { EResource.Tin , 0},
            { EResource.PointCard , 0},
        };
    }

    public Cards(Cards other)
    {
        _resources = new Dictionary<EResource, int>(other._resources);
        _totalPoints = other._totalPoints;
        _ports = new Dictionary<EResource, int>(other._ports);
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
    
    public bool HasPort(EResource eResource)
    {
        return _ports[eResource] > 0;
    }

    public void AddPort(EResource eResource)
    {
        _ports[eResource]++;
    }

    public void RemovePort(EResource eResource)
    {
        if (_ports[eResource] == 0)
        {
            throw new Exception("Can't be with negative amount of ports!");
        }
        _ports[eResource]--;
    }

    public void TransferTotalPoints(int amount)
    {
        if (_totalPoints + amount < 0)
        {
            throw new Exception("Can't be with a negative amount of points!");
        }
        _totalPoints += amount;
    }

    public int Iron
    {
        get => _resources[EResource.Iron];
        set => _resources[EResource.Iron] = value;
    }
    public int Sheep
    {
        get => _resources[EResource.Sheep];
        set => _resources[EResource.Sheep] = value;
    }
    public int Wheat
    {
        get => _resources[EResource.Wheat];
        set => _resources[EResource.Wheat] = value;
    }
    public int Wood
    {
        get => _resources[EResource.Wood];
        set => _resources[EResource.Wood] = value;
    }
    public int Tin
    {
        get => _resources[EResource.Tin];
        set => _resources[EResource.Tin] = value;
    }
}