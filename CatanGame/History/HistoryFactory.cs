using System.Runtime.InteropServices.ComTypes;
using CatanGame.Builder;

namespace CatanGame.History;

public static class HistoryFactory
{
    public static IHistory CreateHistory(Builder.Builder builder)
    {
        return new History(
            builder.FinalScorePath!,
            builder.TurnTimesPath!,
            builder.System!);
    }
    
    public static IHistory CreateSimpleHistory(Builder.Builder builder)
    {
        return new SimpleHistory(builder.TurnTimesPath);
    }
}