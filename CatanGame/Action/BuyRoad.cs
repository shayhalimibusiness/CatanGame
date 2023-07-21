using CatanGame.Enums;
using CatanGame.System;

namespace CatanGame.Action;

public class BuyRoad : IAction 
{
    private ISystem _system;
    private int _x, _y;
    private ERoads _eRoads;
    private EPlayer _ePlayer;
    
    BuyRoad(ISystem system, int x, int y, ERoads eRoads, EPlayer ePlayer)
    {
        _system = system;
        _x = x;
        _y = y;
        _eRoads = eRoads;
        _ePlayer = ePlayer;
    }
    
    public void Do()
    {
        _system.BuildRoad(_x, _y, _eRoads, _ePlayer);
    }

    public void Undo()
    {
        _system.BuildSettlementUndo(_x, _y, _ePlayer);
    }
}