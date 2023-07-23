using CatanGame.Enums;
using CatanGame.System;
using CatanGame.Utils;
using CatanGame.Judge;

namespace CatanGame.UI;

public static class UiTestsPrivates
{
    // public static void Judge_GetNeighborRoads_ShowHorizontalNeighbors()
    // {
    //     var system = SystemFactory.CreateSystem();
    //     var ui = UiFactory.CreateUi();
    //     var neighbors = new Judge.Judge(system, ui, EPlayer.Player1)
    //         .GetNeighborRoads(1, 2, ERoads.Horizontals);
    //     var board = system.GetBoard();
    //     board.SetRoadOwner(1, 2, ERoads.Horizontals, EPlayer.Player1);
    //     var showBoardApi = Mapper.ShowBoardApiMapper(board);
    //     ui.ShowBoard(showBoardApi);
    //     foreach (var neighbor in neighbors)
    //     {
    //         var x = neighbor.Item1;
    //         var y = neighbor.Item2;
    //         var eRoads = neighbor.Item3;
    //         board.SetRoadOwner(x, y, eRoads, EPlayer.Player2);
    //     }
    //
    //     showBoardApi = Mapper.ShowBoardApiMapper(board);
    //     ui.ShowBoard(showBoardApi);
    // }
    //
    // public static void Judge_GetNeighborRoads_ShowVerticalNeighbors()
    // {
    //     var system = SystemFactory.CreateSystem();
    //     var ui = UiFactory.CreateUi();
    //     var neighbors = new Judge.Judge(system, ui, EPlayer.Player1)
    //         .GetNeighborRoads(1, 2, ERoads.Verticals);
    //     var board = system.GetBoard();
    //     board.SetRoadOwner(1, 2, ERoads.Verticals, EPlayer.Player1);
    //     var showBoardApi = Mapper.ShowBoardApiMapper(board);
    //     Console.WriteLine("Before painting the neighbors:");
    //     ui.ShowBoard(showBoardApi);
    //     foreach (var neighbor in neighbors)
    //     {
    //         var x = neighbor.Item1;
    //         var y = neighbor.Item2;
    //         var eRoads = neighbor.Item3;
    //         board.SetRoadOwner(x, y, eRoads, EPlayer.Player2);
    //     }
    //
    //     showBoardApi = Mapper.ShowBoardApiMapper(board);
    //     Console.WriteLine("After painting the neighbors:");
    //     ui.ShowBoard(showBoardApi);
    // }
}