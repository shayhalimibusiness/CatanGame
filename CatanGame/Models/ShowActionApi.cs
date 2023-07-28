using CatanGame.Enums;
using CatanGame.System;
using CatanGame.System.Cards;

namespace CatanGame.Models;

public class ShowActionApi
{
    public EPlayer Player { get; set; } 
    public string? Message { get; set; }
    public ShowBoardApi? ShowBoardApi { get; set; }
    public ICards? Cards { get; set; }
    public Dictionary<EPlayer, ICards>? AllCards { get; set; }
    public ShowStatusApi? ShowStatusApi { get; set; }
}