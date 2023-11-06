using CatanGame.Enums;
using CatanGame.Models;
using CatanGame.System;
using CatanGame.UI;
using CatanGame.Utils;

namespace CatanGame.Action;

public class BuildCity : IAction
{
    private ISystem _system;
    private int _x, _y;
    private EPlayer _ePlayer;
    private IUi _ui;
    
    public BuildCity(ISystem system, IUi ui, int x, int y, EPlayer ePlayer)
    {
        _system = system;
        _ui = ui;
        _x = x;
        _y = y;
        _ePlayer = ePlayer;
    }
    
    public ISystem Do()
    {
        _system.BuildCity(_x, _y, _ePlayer);
        return _system;
    }

    public ISystem Do(ISystem system)
    {
        _system = system;
        return Do();
    }

    public void Undo()
    {
        _system.BuildCityUndo(_x, _y, _ePlayer);
    }

    public void Show()
    {
        var playerName = GeneralFactory.CreatePlayersNames()[_ePlayer];
        var showActionApi = new ShowActionApi
        {
            Message = $"Player: {playerName} built a city at {_x}, {_y}!",
            Player = _ePlayer,
            Cards = _system.GetCards(_ePlayer),
            AllCards = null,
            ShowBoardApi = Mapper.ShowBoardApiMapper(_system.GetBoard()),
            ShowStatusApi = Mapper.ShowStatusApiMapper(_system)
        };
        _ui.ShowAction(showActionApi);
    }
}