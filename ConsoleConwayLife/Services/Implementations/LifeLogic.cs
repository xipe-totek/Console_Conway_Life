using ConsoleConwayLife.Constants;
using ConsoleConwayLife.Models;
using ConsoleConwayLife.Services.Abstractions;

namespace ConsoleConwayLife.Services.Implementations;

public class LifeLogic : ILifeLogic
{
    private int _fieldHeight = Console.WindowHeight;
    private int _fieldWidth = Console.WindowWidth;
    
    /// <summary>
    /// Put neighbours numbers when new cell will be spawned
    /// </summary>
    private IReadOnlyCollection<int> _birthRule = new List<int>() { 3 };
    
    /// <summary>
    /// Put neighbours numbers when cell will survive
    /// </summary>
    private IReadOnlyCollection<int> _surviveRule = new List<int>() { 2, 3 };
    
    // Declare a two-dimensional array.
    private bool[,] _cells;
    
    
    public LifeLogic()
    {
        _cells = new bool[_fieldHeight, _fieldWidth];
    }
    
    public bool IsThereACell(int x, int y)
    {
        // Wrapping space
        int wrappedX = (x + _fieldWidth) % _fieldWidth;
        int wrappedY = (y + _fieldHeight) % _fieldHeight;

        return _cells[wrappedY, wrappedX];
        
    }

    public bool IsNewCellWillBeSpawned(int x, int y)
    {
        return _birthRule.Contains(GetNeighboursCount(x, y));
    }

    public bool IsCellWillSurvive(int x, int y)
    {
        return _surviveRule.Contains(GetNeighboursCount(x, y));
    }

    public void NextStep(int width, int height)
    {
        var nextStepCells = new bool[_fieldHeight, _fieldWidth];
        for (var y = 0; y < _fieldHeight; y++)
        {
            for (var x = 0; x < _fieldWidth; x++)
            {
                if (_cells[y, x])
                {
                    // There is a cell
                    nextStepCells[y, x] = IsCellWillSurvive(x, y);
                }
                else
                {
                    // There is no cell
                    nextStepCells[y, x] = IsNewCellWillBeSpawned(x, y);
                }
            }
        }
        
        for (var y = 0; y < _fieldHeight; y++)
        {
            for (var x = 0; x < _fieldWidth; x++)
            {
                _cells[y, x] = nextStepCells[y, x];
            }
        }
    }

    public void AddCell(int x, int y)
    {
        _cells[y, x] = true;
    }

    public void ClearAllCells()
    {
        for (var y = 0; y < _fieldHeight; y++)
        {
            for (var x = 0; x < _fieldWidth; x++)
            {
                _cells[y, x] = false;
            }
        }
    }

    public int GetNeighboursCount(int x, int y)
    {
        var neighbours = 0;

        if (IsThereACell(x - 1, y - 1)) { neighbours ++; }
        if (IsThereACell(x, y - 1)) { neighbours ++; }
        if (IsThereACell(x + 1, y - 1)) { neighbours ++; }
        if (IsThereACell(x + 1, y)) { neighbours ++; }
        if (IsThereACell(x + 1, y + 1)) { neighbours ++; }
        if (IsThereACell(x, y + 1)) { neighbours ++; }
        if (IsThereACell(x - 1, y + 1)) { neighbours ++; }
        if (IsThereACell(x - 1, y)) { neighbours ++; }
        
        return neighbours;
    }

    public bool[,] ExportCells()
    {
        return _cells;
    }

    public void LoadPatternsFromFile(string fileName)
    {
        string[] lines = File.ReadAllLines(fileName);

        if (lines.Length == 0)
        {
            Console.WriteLine("File is empty.");
            return;
        }
        
        // File check
        for (int i = 0; i < lines.Length; i++)
        {
            string[] elements = lines[i].Split(' ');
            
            // Check for correct string length
            if (lines[i].Length != lines[0].Length)
            {
                Console.WriteLine($"Error: string { i + 1 } has {lines[i].Length} characters. Expected: {lines[0].Length} characters.");
                return;
            }

            // Check if element is a single character
            for (int j = 0; j < elements.Length; j++)
            {
                if (elements[j].Length != 1)
                {
                    Console.WriteLine($"Error: element { j + 1 } in string { i + 1 } has {elements[j].Length} characters. Expected: 1");
                    return;
                }
            }
        }
        
        int patternHeight = lines.Length;
        int patternWidth = lines[0].Split(' ').Length;
        
        // Set pattern position in the middle of screen
        int offsetX = (_fieldWidth  - patternWidth)  / 2;
        int offsetY = (_fieldHeight - patternHeight) / 2;

        for (int y = 0; y < lines.Length; y++)
        {
            string[] elements = lines[y].Split(' ');

            for (int x = 0; x < elements.Length; x++)
            {
                char c = elements[x].Single();

                if (c == '@')
                {
                    // Wrapping space
                    int px = (x + offsetX + _fieldWidth) % _fieldWidth;
                    int py = (y + offsetY + _fieldHeight) % _fieldHeight;

                    _cells[py, px] = true;
                }
            }
        }
    }

    public void ApplyRuleset(Ruleset ruleset)
    {
        _birthRule = ruleset.BirthRule;
        _surviveRule = ruleset.SurviveRule;
    }
}