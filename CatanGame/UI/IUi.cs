using CatanGame.Models;

namespace CatanGame.UI;

public interface IUi
{
    void ShowBoard(ShowBoardApi showBoardApi);
    void ShowDice(int firstCube, int secondCube);
    void ShowAction(ShowActionApi showActionApi);
    void ShowStatus(ShowStatusApi showStatusApi);
}