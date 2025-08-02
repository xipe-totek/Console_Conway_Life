using ConsoleConwayLife.Constants;
using ConsoleConwayLife.Services.Abstractions;

namespace ConsoleConwayLife.Services.Implementations;

public class Surface : ISurface
{
    private bool[,] _oldCells;

    public Surface()
    {
        _oldCells = new bool[Console.WindowHeight, Console.WindowWidth];

        for (var y = 0; y < _oldCells.GetLength(0); y++)
        {
            for (var x = 0; x < _oldCells.GetLength(1); x++)
            {
                _oldCells[y, x] = false;
            }
        }
    }
    
    public void Prepare()
    {
        Console.BackgroundColor = General.GameFieldBackgroundColor;
        Console.ForegroundColor = General.CellColor;
        Console.Clear();
    }

    public void DrawCells(bool[,] cells)
    {
        Console.BackgroundColor = General.GameFieldBackgroundColor;

        for (var y = 0; y < cells.GetLength(0); y++)
        {
            for (var x = 0; x < cells.GetLength(1); x++)
            {
                // 1) Old value is equal to new value - we will do nothing (continue!)
                if (_oldCells[y, x] == cells[y, x])
                {
                    continue;
                }
                
                // 2) Old value is false, new value is true - we will draw new cell
                if (_oldCells[y, x] == false && cells[y, x] == true)
                {
                    Console.SetCursorPosition(x, y);
                    Console.ForegroundColor = General.CellColor;
                    Console.Write("█");
                    continue;
                }
                
                // 3) Old value is true, new value is false - we will clear dead cell
                if (_oldCells[y, x] == true && cells[y, x] == false)
                {
                    Console.SetCursorPosition(x, y);
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    Console.Write("█");
                }
            }
        }
        
        for (var y = 0; y < cells.GetLength(0); y++)
        {
            for (var x = 0; x < cells.GetLength(1); x++)
            {
                _oldCells[y, x] = cells[y, x];
            }
        }
    }

    public void ClearRememberedCells()
    {
        for (var y = 0; y < _oldCells.GetLength(0); y++)
        {
            for (var x = 0; x < _oldCells.GetLength(1); x++)
            {
                _oldCells[y, x] = false;
            }
        }
        
        Prepare();
    }
}