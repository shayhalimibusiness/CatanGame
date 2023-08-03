namespace CatanGame.Models;

public class ShowBoardApi
{
    public const int LinesPerRepresentation = 3;
    public Pixel[,] Pixels { get; set; }
}

public class Pixel
{
    public string? Sign { get; set; }
    public ConsoleColor Color { get; set; }
}

public class Pixels
{
    public string[]? Sign { get; set; }
    public ConsoleColor Color { get; set; }
}