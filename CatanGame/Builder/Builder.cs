using CatanGame.Enums;
using CatanGame.Evaluator;
using CatanGame.Game;
using CatanGame.History;
using CatanGame.Judge;
using CatanGame.Player;
using CatanGame.System;
using CatanGame.System.Board;
using CatanGame.System.Cards;
using CatanGame.UI;

namespace CatanGame.Builder;

public class Builder
{
    public Dictionary<EPlayer, ICards>? AllCards { get; set; }
    public IBoard? Board { get; set; }
    public ISystem? System { get; set; }
    public IUi? Ui { get; set; }
    public IEvaluator? Evaluator { get; set; }
    public IJudge? Judge { get; set; }
    public IEnumerable<IPlayer>? Players { get; set; }
    public IGame? Game { get; set; }
    public IHistory? History { get; set; }
    public string? FinalScorePath { get; set; }
    public string? TurnTimesPath { get; set; }
}