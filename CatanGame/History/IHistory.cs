using CatanGame.Enums;
using CatanGame.Models;

namespace CatanGame.History;

public interface IHistory
{
    void LogScore(EStrategy eStrategy, EPlayer ePlayer);
    void LogTime(EStrategy eStrategy, int round, TimeSpan timeSpan);
    void Save();
}