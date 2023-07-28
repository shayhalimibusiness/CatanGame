using CatanGame.Enums;
using CatanGame.Models;
using CatanGame.System;
using CatanGame.UI;
using CatanGame.Utils;

namespace CatanGame.Action;

public class SetSettlement
{
    private readonly ISystem _system;
    private readonly int _x, _y;
    private readonly EPlayer _ePlayer;
    private readonly IUi _ui;
    
    public SetSettlement(ISystem system, int x, int y, EPlayer ePlayer, IUi ui)
    {
        _system = system;
        _x = x;
        _y = y;
        _ePlayer = ePlayer;
        _ui = ui;
    }
    
    public void Do()
    {
        _system.SetSettlement(_x, _y, _ePlayer);
    }

    public void Undo()
    {
        _system.SetSettlementUndo(_x, _y, _ePlayer);
    }

    public void Show()
    {
        var playerName = GeneralFactory.CreatePlayersNames()[_ePlayer];
        var showActionApi = new ShowActionApi
        {
            Message = $"Player: {playerName} built a settlement at {_x}, {_y}!",
            Player = _ePlayer,
            Cards = _system.GetCards(_ePlayer),
            AllCards = null,
            ShowBoardApi = Mapper.ShowBoardApiMapper(_system.GetBoard()),
            ShowStatusApi = Mapper.ShowStatusApiMapper(_system)
        };
        _ui.ShowAction(showActionApi);
    }
}