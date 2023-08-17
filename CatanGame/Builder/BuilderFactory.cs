using CatanGame.System;

namespace CatanGame.Builder;

public static class BuilderFactory
{
    private const string HistoryDirPath = @"C:\Users\shay.halimi\Documents\Catan\Catan\History";
    
    public static Builder CreateFullBuilder()
    {
        return new Builder()
        {
            FinalScorePath = Path.Combine(HistoryDirPath, "FinalScores.txt"),
            TurnTimesPath = Path.Combine(HistoryDirPath, "TurnTimes.txt"),
            System = SystemFactory.Create1PlayerSystem()
        };
    }
}