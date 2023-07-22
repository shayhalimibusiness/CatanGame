using CatanGame.Enums;
using CatanGame.Models;
using CatanGame.System;
using CatanGame.UI;
using CatanGame.Utils;

namespace CatanGame.Action;

public class BuyCard : IAction
{
    private readonly ISystem _system;
    private readonly EPlayer _ePlayer;
    private bool _isPoint = false;
    private readonly IUi _ui;
    
    public BuyCard(ISystem system, IUi ui, EPlayer ePlayer)
    {
        _system = system;
        _ui = ui;
        _ePlayer = ePlayer;
    }
    
    public void Do()
    {
        _isPoint = _system.BuyCard(_ePlayer);
    }

    public void Undo()
    {
        _system.BuyCardUndo(_ePlayer);
    }

    public void Show()
    {
        var playerName = GeneralFactory.CreatePlayersNames()[_ePlayer];
        var message = _isPoint switch
        {
            true => $"Player: {playerName} bought a card and won a victory point!",
            false => $"Player: {playerName} bought a card, but got nothing."
        };
        var showStatusApi = _isPoint switch
        {
            true => Mapper.ShowStatusApiMapper(_system),
            false => null
        };
        var showActionApi = new ShowActionApi
        {
            Message = message,
            Player = _ePlayer,
            Cards = _system.GetCards(_ePlayer),
            AllCards = null,
            ShowBoardApi = null,
            ShowStatusApi = showStatusApi
        };
        _ui.ShowAction(showActionApi);
    }
}