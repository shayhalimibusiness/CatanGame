using CatanGame.Enums;
using CatanGame.Models;
using CatanGame.System;
using CatanGame.UI;
using CatanGame.Utils;

namespace CatanGame.Action;

public class BuildSettlement : IAction
{
    private ISystem _system;
    private int _x, _y;
    private EPlayer _ePlayer;
    private IUi _ui;
    
    public BuildSettlement(ISystem system, int x, int y, EPlayer ePlayer, IUi ui)
    {
        _system = system;
        _x = x;
        _y = y;
        _ePlayer = ePlayer;
        _ui = ui;
    }
    
    public void Do()
    {
        _system.BuildSettlement(_x, _y, _ePlayer);
    }

    public void Undo()
    {
        _system.BuildSettlementUndo(_x, _y, _ePlayer);
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