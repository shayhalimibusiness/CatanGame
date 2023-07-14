using CatanGame.Models;

namespace CatanGame.History;

public interface IHistory
{
    string ShowLog(int index);
    void LogBoard(LogBoardApi logBoardApi);
    void LogCards(LogCardsApi logCardsApi);
    void LogAllCards(LogAllCardsApi logAllCardsApi);
    void LogStatus(LogStatusApi logStatusApi);
}