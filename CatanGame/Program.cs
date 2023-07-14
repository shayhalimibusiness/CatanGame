// See https://aka.ms/new-console-template for more information

using CatanGame.System;
using CatanGame.Utils;

var board = BoardFactory.CreateExampleBoard();
var showBoardApi = Mapper.ShowBoardApiMapper(board);
Console.WriteLine("Hello, World!");
