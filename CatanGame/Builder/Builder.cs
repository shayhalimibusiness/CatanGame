using CatanGame.System;

namespace CatanGame.Builder;

public class Builder
{
    public ISystem? System { get; set; }
    public string? FinalScorePath { get; set; }
    public string? TurnTimesPath { get; set; }
}