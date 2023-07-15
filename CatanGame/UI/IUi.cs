using System.ComponentModel.Design;
using CatanGame.Enums;
using CatanGame.Models;
using CatanGame.System;

namespace CatanGame.UI;

public interface IUi
{
    void ShowBoard(ShowBoardApi showBoardApi);
    void ShowDice(int firstCube, int secondCube);
    void ShowAction(ShowActionApi showActionApi);
    void ShowStatus(ShowStatusApi showStatusApi);
    void ShowCards(ICards cards, EPlayer player);
    void ShowAllCards(Dictionary<EPlayer, ICards> allCards);
}