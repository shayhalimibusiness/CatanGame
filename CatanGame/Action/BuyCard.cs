using CatanGame.Enums;
using CatanGame.System;

namespace CatanGame.Action;

public class BuyCard : IAction
{
    private ISystem _system;
    private EPlayer _ePlayer;
    
    public BuyCard(ISystem system, EPlayer ePlayer)
    {
        _system = system;
        _ePlayer = ePlayer;
    }
    
    public void Do()
    {
        _system.BuyCard(_ePlayer);
    }

    public void Undo()
    {
        _system.BuyCardUndo(_ePlayer);
    }

    public void Show()
    {
        throw new NotImplementedException();
    }
}