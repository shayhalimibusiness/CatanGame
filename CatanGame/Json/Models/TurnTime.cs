using CatanGame.Enums;

namespace CatanGame.DataBaseProxy.Models;

public class TurnTime
{
    public EStrategy EStrategy { get; set; }
    public int Round { get; set; }
    public TimeSpan Time { get; set; }
}