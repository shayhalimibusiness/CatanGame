using System.Runtime.InteropServices.ComTypes;

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
}