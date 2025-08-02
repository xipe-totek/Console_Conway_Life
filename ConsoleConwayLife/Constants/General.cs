namespace ConsoleConwayLife.Constants;

public class General
{
    /// <summary>
    /// Game field background color
    /// </summary>
    public const ConsoleColor GameFieldBackgroundColor = ConsoleColor.Black;
    
    /// <summary>
    /// Cells color
    /// </summary>
    public const ConsoleColor CellColor = ConsoleColor.Cyan;
    
    /// <summary>
    /// Text color
    /// </summary>
    public const ConsoleColor TextColor = ConsoleColor.Cyan;

    /// <summary>
    /// For timer control
    /// </summary>
    public const int DefaultTimer = 100;
    public const int MinTimer = 10;
    public const int MaxTimer = 2000;
    
    /// <summary>
    /// For CreateCells method
    /// </summary>
    public const int MinCellsNumber = 2000;
    public const int MaxCellsNumber = 4000;
}