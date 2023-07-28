using CatanGame.Enums;
using CatanGame.Models;
using CatanGame.System;
using CatanGame.UI;
using CatanGame.Utils;

namespace CatanGame.Action;

public class SetRoad
{
    private readonly ISystem _system;
    private readonly IUi _ui;
    private readonly int _x, _y;
    private readonly ERoads _eRoads;
    private readonly EPlayer _ePlayer;
    
    public SetRoad(ISystem system, IUi ui, int x, int y, ERoads eRoads, EPlayer ePlayer)
    {
        _system = system;
        _ui = ui;
        _x = x;
        _y = y;
        _eRoads = eRoads;
        _ePlayer = ePlayer;
    }
    
    public void Do()
    {
        _system.SetRoad(_x, _y, _eRoads, _ePlayer);
    }

    public void Undo()
    {
        _system.SetRoadUndo(_x, _y, _eRoads, _ePlayer);
    }

    public void Show()
    {
        var playerName = GeneralFactory.CreatePlayersNames()[_ePlayer];
        var roadName = GeneralFactory.CreateRoadsNames()[_eRoads];
        var showActionApi = new ShowActionApi
        {
            Message = $"Player: {playerName} built a {roadName} road at {_x}, {_y}!",
            Player = _ePlayer,
            Cards = _system.GetCards(_ePlayer),
            AllCards = null,
            ShowBoardApi = Mapper.ShowBoardApiMapper(_system.GetBoard()),
            ShowStatusApi = null
        };
        _ui.ShowAction(showActionApi);
    }
}