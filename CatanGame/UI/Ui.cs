using CatanGame.Enums;
using CatanGame.Models;
using CatanGame.System;
using CatanGame.System.Cards;
using CatanGame.Utils;

namespace CatanGame.UI;

public class Ui : IUi
{
    private Dictionary<EPlayer, string> _names;

    public Ui(Dictionary<EPlayer, string> names)
    {
        _names = names;
    }

    public void ShowBoard(ShowBoardApi showBoardApi)
    {
        const int boardSize = 9;
        Console.WriteLine();
        for (var i = 0; i < boardSize; i++)
        {
            for (var j = 0; j < boardSize; j++)
            {
                Console.ForegroundColor = showBoardApi.Pixels[i, j].Color;
                Console.Write(showBoardApi.Pixels[i, j].Sign);
            }
            Console.WriteLine();
        }
    }

    public void ShowDice(int firstCube, int secondCube)
    {
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"Dice Role: {firstCube} and {secondCube}");
    }

    public void ShowAction(ShowActionApi showActionApi)
    {
        Console.WriteLine();
        var foregroundColor = Mapper.GetPlayerColor(showActionApi.Player);
        Console.ForegroundColor = foregroundColor;
        Console.WriteLine(showActionApi.Message);
        if (showActionApi.ShowBoardApi != null)
        {
            Console.WriteLine("Board after action:");
            ShowBoard(showActionApi.ShowBoardApi);
        }

        if (showActionApi.Cards != null)
        {
            ShowCards(showActionApi.Cards, showActionApi.Player);
        }

        if (showActionApi.AllCards != null)
        {
            ShowAllCards(showActionApi.AllCards);
        }

        if (showActionApi.ShowStatusApi != null)
        {
            ShowStatus(showActionApi.ShowStatusApi);
        }
    }

    public void ShowStatus(ShowStatusApi showStatusApi)
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine();
        foreach (var player in showStatusApi.TotalPoints.Keys)
        {
            Console.WriteLine($"{_names[player]} has {showStatusApi.TotalPoints[player]} points.");
        }
    }

    public void ShowCards(ICards cards, EPlayer player)
    {
        Console.ForegroundColor = Mapper.GetPlayerColor(player);
        Console.WriteLine();
        var resourceNames = GeneralFactory.CreateResourcesNames();
        var resourcesAmount = cards.GetResources();
        var totalPoints = cards.GetTotalPoints();
        Console.WriteLine($"{_names[player]} Cards and Total Points:");
        foreach (var pair in resourcesAmount)
        {
            var resource = pair.Key;
            var name = resourceNames[resource];
            Console.WriteLine($"{name}: {resourcesAmount[resource]}");
        }
        Console.WriteLine($"Total Points: {totalPoints}");
    }

    public void ShowAllCards(Dictionary<EPlayer, ICards> allCards)
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine();
        Console.WriteLine("All Cards:");
        foreach (var pair in allCards)
        {
            var player = pair.Key;
            var cards = pair.Value;
            ShowCards(cards, player);
        }
    }
}