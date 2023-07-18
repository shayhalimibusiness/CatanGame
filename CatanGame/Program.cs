// See https://aka.ms/new-console-template for more information

using CatanGame.Enums;
using CatanGame.Models;
using CatanGame.System;
using CatanGame.UI;
using CatanGame.Utils;








Console.WriteLine("Hello, World!");

var uiTest = new UiTests();
// uiTest.ApiTests();
uiTest.ShowBoard_GetRandomBoard_Show();
uiTest.ShowDice_GetSystemRoll_Show();