using ConsoleConwayLife.Models;

namespace ConsoleConwayLife.Services.Abstractions;

public interface ILifeLogic
{
    /// <summary>
    /// Make next step
    /// </summary>
    void NextStep(int width, int height);
    
    #region Export-Import

    /// <summary>
    /// Add cell to field
    /// </summary>
    void AddCell(int x, int y);
    
    /// <summary>
    /// Remove all cells
    /// </summary>
    void ClearAllCells();
    
    /// <summary>
    /// Export cells to outer world
    /// </summary>
    bool[,] ExportCells();

    /// <summary>
    /// Load pattern from given file
    /// </summary>
    void LoadPatternsFromFile(string fileName);

    /// <summary>
    /// Apply new ruleset
    /// </summary>
    void ApplyRuleset(Ruleset ruleset);

    #endregion
}