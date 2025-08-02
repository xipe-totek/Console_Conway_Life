namespace ConsoleConwayLife.Services.Abstractions;

public interface ISurface
{
    /// <summary>
    /// Prepare the game field
    /// </summary>
    void Prepare();
    
    /// <summary>
    /// Draw cells from array
    /// </summary>
    /// <param name="cells">Cells array</param>
    void DrawCells(bool[,] cells);
    
    /// <summary>
    /// Remove all cells
    /// </summary>
    void ClearRememberedCells();
}