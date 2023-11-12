using System.Formats.Asn1;
using CatanGame.Enums;
using CatanGame.Json;
using Microsoft.IdentityModel.Tokens;

namespace CatanGame.History;

public class SimpleHistory : IHistory
{
    private JsonFileManager<List<TurnTimes>> _graphLogger;
    private decimal[] RoundTimes { get; set; } = new decimal[100];
    private int Rounds { get; set; } = 0;
    private int Games { get; set; } = 0;

    public SimpleHistory(string filePath)
    {
        _graphLogger = new JsonFileManager<List<TurnTimes>>(filePath);
    }

    public void LogScore(EStrategy eStrategy, EPlayer ePlayer)
    {
        throw new NotImplementedException();
    }

    public void LogTime(EStrategy eStrategy, int round, TimeSpan timeSpan)
    {
        if (Games == 0)
        {
            RoundTimes[round] = 0;
        }

        RoundTimes[round] += timeSpan.Milliseconds;
    }

    public void Save()
    {
        Games++;
        if (Games != 15)
        {
            return;
        }
        var graph = new List<TurnTimes>();
        for (var i = 0; i < 100; i++)
        {
            graph.Add(new TurnTimes
            {
                Round = i,
                Time = RoundTimes[i] /= Games
            });
        }
        _graphLogger.Save(graph);
    }
}

class TurnTimes
{
    public int Round { get; set; }    
    public decimal Time { get; set; }    
}