using CatanGame.Enums;
using CatanGame.System;

namespace CatanGame.Action;

public class BuildCity : IAction
{
    private ISystem _system;
    private int _x, _y;
    private EPlayer _ePlayer;
    
    public BuildCity(ISystem system, int x, int y, EPlayer ePlayer)
    {
        _system = system;
        _x = x;
        _y = y;
        _ePlayer = ePlayer;
    }
    
    public void Do()
    {
        _system.BuildCity(_x, _y, _ePlayer);
    }

    public void Undo()
    {
        _system.BuildCityUndo(_x, _y, _ePlayer);
    }

    public void Show()
    {
        throw new NotImplementedException();
    }
}