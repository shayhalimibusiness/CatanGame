using CatanGame.DataBaseProxy.Models;
using CatanGame.Enums;
using CatanGame.Json;
using CatanGame.System;

namespace CatanGame.History;

public class History : IHistory
{
    private JsonFileManager<List<FinalScore>> _finalScoreProxy;
    private JsonFileManager<List<TurnTime>> _turnTimesProxy;

    private ISystem _system;

    private List<FinalScore> _finalScores;
    private List<TurnTime> _turnTimes;

    public History(string finalScoresPath, string turnTimesPath, ISystem system)
    {
        _finalScoreProxy = new JsonFileManager<List<FinalScore>>(finalScoresPath);
        _turnTimesProxy = new JsonFileManager<List<TurnTime>>(turnTimesPath);

        _finalScores = _finalScoreProxy.Load() ?? new List<FinalScore>();
        _turnTimes = _turnTimesProxy.Load() ?? new List<TurnTime>();
        
        _system = system;
    }

    public void LogScore(EStrategy eStrategy, EPlayer ePlayer)
    {
        _finalScores.Add(new FinalScore
        {
            EStrategy = eStrategy,
            Score = _system.GetAllCards()[ePlayer].GetTotalPoints()
        });
    }

    public void LogTime(EStrategy eStrategy, int round, TimeSpan timeSpan)
    {
        _turnTimes.Add(new TurnTime
        {
            EStrategy = eStrategy,
            Round = round,
            Time = timeSpan
        });
    }

    public void Save()
    {
        _finalScoreProxy.Save(_finalScores);
        _turnTimesProxy.Save(_turnTimes);
    }
}