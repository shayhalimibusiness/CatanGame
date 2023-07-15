using CatanGame.Enums;
using CatanGame.Models;
using CatanGame.System;
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
        foreach (var pair in resourceNames)
        {
            var resource = pair.Key;
            var name = pair.Value;
            Console.WriteLine($"{name}: {resourcesAmount[resource]}");
        }
        Console.WriteLine($"Total Points: {totalPoints}");
    }

    public void ShowAllCards(Dictionary<EPlayer, ICards> allCards)
    {
        throw new NotImplementedException();
    }
}