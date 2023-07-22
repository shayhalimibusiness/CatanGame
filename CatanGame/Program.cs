// See https://aka.ms/new-console-template for more information

using CatanGame.Enums;
using CatanGame.Models;
using CatanGame.System;
using CatanGame.UI;
using CatanGame.Utils;








Console.WriteLine("Hello, World!");

var uiTest = new UiTests();
// uiTest.ApiTests();
// uiTest.ShowBoard_GetRandomBoard_Show();
// uiTest.ShowDice_GetSystemRoll_Show();
// uiTest.ShowStatusApi_SystemGetGameSummery_Show();
// uiTest.Action_BuildSettlement_Show();
// uiTest.Action_BuildCity_Show();
// uiTest.Action_BuildRoad_Show();
uiTest.Action_BuyCard_Show_10Times();