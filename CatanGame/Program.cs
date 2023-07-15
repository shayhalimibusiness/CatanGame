// See https://aka.ms/new-console-template for more information

using CatanGame.Models;
using CatanGame.System;
using CatanGame.UI;
using CatanGame.Utils;

var board = BoardFactory.CreateExampleBoard();
var showBoardApi = Mapper.ShowBoardApiMapper(board);
var ui = UiFactory.CreateUi();
ui.ShowBoard(showBoardApi);
ui.ShowDice(5, 6);
var showActionApi = ModelFactory.CreateShowActionApiExample1();
ui.ShowAction(showActionApi);
var showStatusApi = ModelFactory.CreateShowStatusApiExample();
ui.ShowStatus(showStatusApi);
Console.WriteLine("Hello, World!");
