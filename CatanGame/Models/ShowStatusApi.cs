using CatanGame.Enums;

namespace CatanGame.Models;

public class ShowStatusApi
{
    public Dictionary<EPlayer, int>? TotalPoints { get; set; }
}