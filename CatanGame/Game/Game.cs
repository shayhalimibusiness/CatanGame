using System.Diagnostics;
using CatanGame.Enums;
using CatanGame.History;
using CatanGame.Player;
using CatanGame.System;
using CatanGame.UI;
using CatanGame.Utils;

namespace CatanGame.Game;

public class Game : IGame
{
    private readonly ISystem _system;
    private readonly IUi _ui;
    private readonly IHistory _history;
    private readonly IEnumerable<IPlayer> _players;
    private readonly Stopwatch _stopwatch;

    public Game(ISystem system, IUi ui, IEnumerable<IPlayer> players, IHistory history)
    {
        _system = system;
        _ui = ui;
        _history = history;
        _players = players;
        _stopwatch = new Stopwatch();
    }
    
    public int Run()
    {
        var names = GeneralFactory.CreatePlayersNames();
        for (var i = 0; i < 100; i++)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine();
            Console.WriteLine($"This is round number {i}.");
            var playerIndex = 0;
            foreach (var player in _players)
            {
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.Write($"This is {names[(EPlayer)playerIndex]} turn.");
                _stopwatch.Start();
                if (i is 0 or 1)
                {
                    player.PlayStartTurn();
                }
                else
                {
                    _system.RoleDice();
                    player.Play();
                }
                _stopwatch.Stop();
                _history.LogTime(EStrategy.Heuristic, i, _stopwatch.Elapsed);
                _stopwatch.Restart();
                playerIndex++;
            }
            _ui.ShowStatus(Mapper.ShowStatusApiMapper(_system));
            _ui.ShowAllCards(_system.GetAllCards());
        }
        _history.Save();
        return _system.GetAllCards()[EPlayer.Player1].GetTotalPoints();
    }
}