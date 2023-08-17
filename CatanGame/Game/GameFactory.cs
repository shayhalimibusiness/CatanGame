using CatanGame.Enums;
using CatanGame.Player;
using CatanGame.System.Board;
using CatanGame.System.Cards;
using CatanGame.Utils;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CatanGame.Game;

public static class GameFactory
{
    public static IGame Create1PlayerGame(Builder.Builder builder)
    {
        return new Game(
            builder.System!, 
            builder.Ui!, 
            builder.Players!, 
            builder.History!);
    }
}