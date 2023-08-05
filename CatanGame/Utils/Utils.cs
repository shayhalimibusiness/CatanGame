namespace CatanGame.Utils;

public static class Utils
{
    public static decimal Probability(int sum)
    {
        switch (sum)
        {
            case <= 1:
            case >= 13:
                return 0;
            case <= 7:
                return (decimal)(sum - 1) / 36;
            default:
                return (decimal)(13 - sum) / 36;
        }
    }
}