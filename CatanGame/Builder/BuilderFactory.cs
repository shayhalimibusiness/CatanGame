using CatanGame.Enums;
using CatanGame.Game;
using CatanGame.History;
using CatanGame.Player;
using CatanGame.System;
using CatanGame.System.Board;
using CatanGame.System.Cards;
using CatanGame.Utils;

namespace CatanGame.Builder;

public static class BuilderFactory
{
    private const string HistoryDirPath = @"C:\Users\shay.halimi\Documents\Catan";

    public static Builder Create1PlayerEmptyGameFullBuilder()
    {
        var builder = new Builder()
        {
            FinalScorePath = Path.Combine(HistoryDirPath, "FinalScores.txt"),
            TurnTimesPath = Path.Combine(HistoryDirPath, "TurnTimes.txt"),
            Board = BoardFactory.CreateRandomBoard(),
            AllCards = CardsFactory.Create1PlayerBlankAllCards(),
            Ui = UiFactory.CreateUi(),
        };
        builder.System = new System.System(builder.Board, builder.AllCards, builder.Ui); 
        builder.Evaluator = new Evaluator.Evaluator(builder.System, EPlayer.Player1);
        builder.Judge = new Judge.Judge(builder.System, builder.Ui, EPlayer.Player1);
        builder.History = HistoryFactory.CreateHistory(builder);
        
        return builder;
    }
    
    public static Builder Create1HeuristicPlayerFullBuilder()
    {
        var builder = Create1PlayerEmptyGameFullBuilder();
        builder.Players = new List<IPlayer> { new HeuristicPlayer(builder.Judge!, builder.Evaluator!) };
        builder.Game = GameFactory.Create1PlayerGame(builder);
        
        return builder;
    }
    
    public static Builder Create1ExpectimaxPlayerFullBuilder()
    {
        var builder = BuilderFactory.Create1PlayerEmptyGameFullBuilder();
        builder.Players = new List<IPlayer>
        {
            new ExpectimaxPlayer(
                builder.Judge!, 
                builder.Evaluator!,
                builder.System!)
        };
        builder.Game = GameFactory.Create1PlayerGame(builder);
        
        return builder;
    }
}