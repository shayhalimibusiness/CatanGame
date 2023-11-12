// See https://aka.ms/new-console-template for more information

using CatanGame.Builder;
using CatanGame.DataBaseProxy;
using CatanGame.DataBaseProxy.Models;
using CatanGame.Enums;
using CatanGame.Game;
using CatanGame.History;
using CatanGame.Json;
using CatanGame.UI;

Console.WriteLine("Hello, World!");

var builder = BuilderFactory.Create1ParallelExpectimaxPlayerFullBuilder();
var history = HistoryFactory.CreateSimpleHistory(builder);
for (var i = 0; i < 17; i++)
{
    builder = BuilderFactory.Create1ParallelExpectimaxPlayerFullBuilder();
    builder.History = history;
    var game = GameFactory.Create1PlayerGame(builder);
    game.Run();
    Console.WriteLine($"Game number: {i}");
}


// var jsonFileManager = new JsonFileManager<FinalScore>(@"C:\Users\shay.halimi\Desktop\ddd.txt");
// jsonFileManager.Save(new FinalScore
// {
//     EStrategy = EStrategy.Expectimax,
//     Score = 4
// });
// Console.WriteLine(jsonFileManager.Load().EStrategy);
//
// var history = HistoryFactory.CreateHistory(BuilderFactory.Create1HeuristicPlayerFullBuilder());
// history.LogScore(EStrategy.Expectimax, EPlayer.Player1);
// history.Save();