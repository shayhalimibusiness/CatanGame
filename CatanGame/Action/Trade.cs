using CatanGame.Enums;
using CatanGame.System;

namespace CatanGame.Action;

public class Trade : IAction
{
    private ISystem _system;
    private EPlayer _ePlayer;
    private EResource _sell;
    private EResource _buy;
    private int _amount;
    
    public Trade(ISystem system, EPlayer ePlayer, EResource sell, EResource buy, int amount)
    {
        _system = system;
        _ePlayer = ePlayer;
        _sell = sell;
        _buy = buy;
        _amount = amount;
    }
    
    public void Do()
    {
        _system.Trade(_ePlayer, _sell, _buy, _amount);
    }

    public void Undo()
    {
        _system.TradeUndo(_ePlayer, _sell, _buy, _amount);
    }

    public void Show()
    {
        throw new NotImplementedException();
    }
}