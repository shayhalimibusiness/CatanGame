using CatanGame.Enums;
using CatanGame.Player;
using CatanGame.System;
using CatanGame.UI;
using CatanGame.Utils;

namespace CatanGame.Game;

public class Game : IGame
{
    private readonly ISystem _system;
    private readonly IUi _ui;
    private readonly IEnumerable<IPlayer> _players;

    public Game(ISystem system, IUi ui, IEnumerable<IPlayer> players)
    {
        _system = system;
        _ui = ui;
        _players = players;
    }
    
    public void Run()
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
                if (i is 0 or 1)
                {
                    player.PlayStartTurn();
                }
                else
                {
                    _system.RoleDice();
                    player.Play();
                }
                playerIndex++;
            }
            _ui.ShowStatus(Mapper.ShowStatusApiMapper(_system));
            _ui.ShowAllCards(_system.GetAllCards());
        }
    }
}