namespace CatanGame.Models;

public class ShowBoardApi
{
    public Pixel[,] Pixels { get; set; }
}

public class Pixel
{
    public string? Sign { get; set; }
    public ConsoleColor Color { get; set; }
}