using System.Timers;
using ConsoleConwayLife.Constants;
using ConsoleConwayLife.Services.Abstractions;
using ConsoleConwayLife.Services.Implementations;
using Timer = System.Timers.Timer;

namespace ConsoleConwayLife;

class Program
{
    private static ISurface _surface = new Surface();
    
    private static ILifeLogic _lifeLogic = new LifeLogic();
    
    private static IRulesetsLoader _rulesetsLoader = new RulesetsLoader();
    
    private static IMenu _menu = new Menu();
    
    private static int _interval = General.DefaultTimer;
    private static Timer _nextStepTimer = new Timer(_interval);
    
    private static int _generation = 0;
    private static string _pattern = "";
    private static string _rule = "";
    
    
    static void Main(string[] args)
    {
        CreateCells();
        
        _surface.Prepare();
        
        _surface.DrawCells(_lifeLogic.ExportCells());
        
        _nextStepTimer.AutoReset = true;
        _nextStepTimer.Elapsed += NextStepTimerOnElapsed;
        _nextStepTimer.Enabled = true;
        
        while (true)
        {
            if (!Console.KeyAvailable)
            {
                continue;
            }
            
            var key = Console.ReadKey(true);
            
            // Check for Control-C
            if (key.Key == ConsoleKey.C && key.Modifiers == ConsoleModifiers.Control)
            {
                Environment.Exit(0);
            }

            if (key.Key == ConsoleKey.R)
            {
                Restart();
            }
            
            if (key.Key == ConsoleKey.M)
            {
                ShowLoadingMenu();
            }
            
            if (key.Key == ConsoleKey.Q)
            {
                _interval /= 2;

                if (_interval <= General.MinTimer)
                {
                    _interval = General.MinTimer;
                }
                _nextStepTimer.Stop();
                _nextStepTimer.Interval = _interval;
                _nextStepTimer.Start();
            }
            
            if (key.Key == ConsoleKey.W)
            {
                _interval *= 2;

                if (_interval >= General.MaxTimer)
                {
                    _interval = General.MaxTimer;
                }
                _nextStepTimer.Stop();
                _nextStepTimer.Interval = _interval;
                _nextStepTimer.Start();
            }

            if (key.Key == ConsoleKey.Spacebar)
            {
                if (_nextStepTimer.Enabled == true)
                {
                    _nextStepTimer.Enabled = false;
                }
                else
                {
                    _nextStepTimer.Enabled = true;
                }
            }

            if (_nextStepTimer.Enabled == false && key.Key == ConsoleKey.Enter)
            {
                _lifeLogic.NextStep(Console.WindowWidth, Console.WindowHeight);
                _surface.DrawCells(_lifeLogic.ExportCells());
                DisplayStatistics();
                _generation++;
            }

            if (key.Key == ConsoleKey.B)
            {
                ShowRulesetsMenu();
            }
        }
    }

    private static void NextStepTimerOnElapsed(object? sender, ElapsedEventArgs e)
    {
        _lifeLogic.NextStep(Console.WindowWidth, Console.WindowHeight);
        _surface.DrawCells(_lifeLogic.ExportCells());
        DisplayStatistics();
        _generation++;
    }

    private static void CreateCells()
    {
        var random = new Random();
        var numberOfCells = random.Next(General.MinCellsNumber, General.MaxCellsNumber);
        var maxX = Console.WindowWidth;
        var maxY = Console.WindowHeight;
        var cellsCount = 0;
        
        while (cellsCount < numberOfCells)
        {
            int x = random.Next(0, maxX);
            int y = random.Next(0, maxY);

            _lifeLogic.AddCell(x, y);
            
            cellsCount++;
        }
    }

    private static void Restart()
    {
        _lifeLogic.ClearAllCells();
        _surface.ClearRememberedCells();
        _generation = 0;
        CreateCells();
    }

    private static void ShowLoadingMenu()
    {
        _nextStepTimer.Stop();
        _lifeLogic.ClearAllCells();
        _surface.ClearRememberedCells();
        
        var patternFolder = "patterns";
        
        string[] files = Directory.GetFiles(patternFolder);
        
        var selectedIndex = _menu.ShowMenuAndReturnUserChoice
        (
            files
                .Select(f => Path.GetFileNameWithoutExtension(f))
                .ToList()
        );
        
        Console.Clear();
        _surface.Prepare();
        _generation = 0;
        _pattern = files[selectedIndex];
        _lifeLogic.LoadPatternsFromFile(files[selectedIndex]);
        _nextStepTimer.Start();
    }
    
    private static void ShowRulesetsMenu()
    {
        _nextStepTimer.Stop();
        
        var menuItems = _rulesetsLoader
            .LoadRulesetsFromDirectory("rules")
            .ToList();
        
        var selectedRulesetIndex = _menu.ShowMenuAndReturnUserChoice(menuItems.Select(m => m.Name).ToList());
        
        _lifeLogic.ApplyRuleset(_rulesetsLoader.LoadRuleset(menuItems[selectedRulesetIndex].Path));
        
        Console.Clear();
        _rule = menuItems[selectedRulesetIndex].Name;
        _nextStepTimer.Start();
    }
    
    public static void DisplayStatistics()
    {
        string text = $"[ Game speed: { _interval } ms ]";
        Console.SetCursorPosition(Console.WindowWidth - text.Length, 0);
        Console.BackgroundColor = ConsoleColor.Gray;
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write(text);

        string textGen = $"[ Generation: { _generation } ]";
        Console.SetCursorPosition(0, 0);
        Console.BackgroundColor = ConsoleColor.Gray;
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write(textGen);
        
        string textPattern = $"[ Pattern: { _pattern } ]";
        Console.SetCursorPosition(0, Console.WindowHeight);
        Console.BackgroundColor = ConsoleColor.Gray;
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write(textPattern);
        
        string textRule = $"[ Rule: { _rule } ]";
        Console.SetCursorPosition(Console.WindowWidth - textRule.Length, Console.WindowHeight);
        Console.BackgroundColor = ConsoleColor.Gray;
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write(textRule);
    }
}