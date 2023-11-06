using CatanGame.Enums;
using CatanGame.Models;
using CatanGame.System;
using CatanGame.UI;
using CatanGame.Utils;

namespace CatanGame.Action;

public class BuildRoad : IAction 
{
    private ISystem _system;
    private IUi _ui;
    private int _x, _y;
    private ERoads _eRoads;
    private EPlayer _ePlayer;
    
    public BuildRoad(ISystem system, IUi ui, int x, int y, ERoads eRoads, EPlayer ePlayer)
    {
        _system = system;
        _ui = ui;
        _x = x;
        _y = y;
        _eRoads = eRoads;
        _ePlayer = ePlayer;
    }
    
    public ISystem Do()
    {
        _system.BuildRoad(_x, _y, _eRoads, _ePlayer);
        return _system;
    }

    public void Undo()
    {
        _system.BuildRoadUndo(_x, _y, _eRoads, _ePlayer);
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