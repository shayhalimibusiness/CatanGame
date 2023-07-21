using CatanGame.Enums;
using CatanGame.Models;
using CatanGame.System;
using CatanGame.Utils;

namespace CatanGame.UI;

public class UiTests
{
    private IUi _ui;
    private ISystem _system;

    public UiTests()
    {
        _ui = UiFactory.CreateUi();
        _system = SystemFactory.CreateSystem();
    }

    public void ApiTests()
    {
        ShowBoard_GetShowBoardApi_Show();
        ShowDice_GetDice_Show();
        ShowAction_GetShowActionApi_Show();
        ShowCards_GetCards_Show();
        ShowStatus_GetShowStatusApi_Show();
        ShowAllCards_GetAllCards_Show();
    }
    
    public void ShowBoard_GetShowBoardApi_Show()
    {
        var board = BoardFactory.CreateExampleBoard();
        var showBoardApi = Mapper.ShowBoardApiMapper(board);
        _ui.ShowBoard(showBoardApi);
    }

    public void ShowDice_GetDice_Show()
    {
        _ui.ShowDice(5, 6);
    }

    public void ShowAction_GetShowActionApi_Show()
    {
        var showActionApi = ModelFactory.CreateShowActionApiExample1();
        _ui.ShowAction(showActionApi);
    }

    public void ShowStatus_GetShowStatusApi_Show()
    {
        var showStatusApi = ModelFactory.CreateShowStatusApiExample();
        _ui.ShowStatus(showStatusApi);
    }

    public void ShowCards_GetCards_Show()
    {
        var cards = CardsFactory.CreateCardsExample1();
        _ui.ShowCards(cards, EPlayer.Player2);
    }

    public void ShowAllCards_GetAllCards_Show()
    {
        var allCards = CardsFactory.CreateAllCardsExample();
        _ui.ShowAllCards(allCards);
    }

    public void ShowBoard_GetRandomBoard_Show()
    {
        var randomBoard = BoardFactory.CreateRandomBoard();
        var showBoardApi = Mapper.ShowBoardApiMapper(randomBoard);
        _ui.ShowBoard(showBoardApi);
    }

    public void ShowDice_GetSystemRoll_Show()
    {
        _system.RoleDice();
    }

    public void ShowStatusApi_SystemGetGameSummery_Show()
    {
        var player1Cards = _system.GetCards(EPlayer.Player1);
        var player2Cards = _system.GetCards(EPlayer.Player2);
        player1Cards.TransferTotalPoints(2);
        player2Cards.TransferTotalPoints(3);
        _system.GetGameSummery();
    }
}