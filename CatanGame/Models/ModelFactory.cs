using CatanGame.Enums;
using CatanGame.System;
using CatanGame.Utils;

namespace CatanGame.Models;

public static class ModelFactory
{
    public static ShowActionApi CreateShowActionApiExample1()
    {
        var showActionApi = new ShowActionApi
        {
            Player = EPlayer.Player1,
            Message = "Here is an action I made.",
            ShowBoardApi = Mapper.ShowBoardApiMapper(BoardFactory.CreateExampleBoard())
        };
        return showActionApi;
    }

    public static ShowStatusApi CreateShowStatusApiExample()
    {
        var showStatusApi = new ShowStatusApi
        {
            TotalPoints = new Dictionary<EPlayer, int>
            {
                { EPlayer.Player1, 3 },
                { EPlayer.Player2, 5 }
            }
        };

        return showStatusApi;
    }
}