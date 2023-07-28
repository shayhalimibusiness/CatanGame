using CatanGame.Enums;
using CatanGame.Models;
using CatanGame.System;
using CatanGame.UI;
using CatanGame.Utils;

namespace CatanGame.Action;

public class Trade : IAction
{
    private readonly ISystem _system;
    private readonly IUi _ui;
    private readonly EPlayer _ePlayer;
    private readonly EResource _sell;
    private readonly EResource _buy;
    private readonly int _amount;

    public Trade(ISystem system, IUi ui, EPlayer ePlayer, EResource sell, EResource buy, int amount) 
    {
        _system = system;
        _ui = ui;
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
        var playerName = GeneralFactory.CreatePlayersNames()[_ePlayer];
        var resourcesName = GeneralFactory.CreateResourcesNames();
        var sellResourceName = resourcesName[_sell];
        var buyResourceName = resourcesName[_buy];
        var marketRate = 4;
        if (_system.HasPort(_ePlayer, _sell))
        {
            marketRate = 2;
        }
        else if (_system.HasPort(_ePlayer, EResource.PointCard))
        {
            marketRate = 3;
        }
        var showActionApi = new ShowActionApi
        {
            Message = $"{playerName} has traded {marketRate*_amount} {sellResourceName} for {_amount} {buyResourceName}!",
            Player = _ePlayer,
            Cards = _system.GetCards(_ePlayer),
            AllCards = null,
            ShowBoardApi = null,
            ShowStatusApi = null
        };
        _ui.ShowAction(showActionApi);
    }
}