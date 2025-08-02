using ConsoleConwayLife.Services.Abstractions;

namespace ConsoleConwayLife.Services.Implementations;

public class Menu : IMenu
{
    /// <summary>
    /// User error delegate for EnterNumber()
    /// </summary>
    private delegate void OnUserErrorDelegate(Object data);
    
    public int ShowMenuAndReturnUserChoice(IReadOnlyCollection<string> items)
    {
        var processedItems = new List<MenuItem>();

        for (var i = 0; i < items.Count; i++)
        {
            processedItems.Add(new MenuItem() { OriginalIndex = i, Text = items.ToList()[i]});
        }
        
        processedItems = processedItems
            .OrderBy(x => x.Text)
            .ToList();

        var itemsToDisplay = new List<string>();
        for (var i = 0; i < processedItems.Count; i++)
        {
            itemsToDisplay.Add($"║ { i + 1 }. { processedItems[i].Text }");
        }
        
        var longestItemLenght = itemsToDisplay
            .Max(i => i.Length)
            + 2; // For right border

        var menuHeight = itemsToDisplay.Count + 3;
        
        var shiftX = (Console.WindowWidth - longestItemLenght) / 2;
        var shiftY = (Console.WindowHeight - menuHeight) / 2;
        
        ClearRectangle(shiftX, shiftY, longestItemLenght, menuHeight, ConsoleColor.DarkBlue);
        Console.BackgroundColor = ConsoleColor.DarkBlue;
        Console.ForegroundColor = ConsoleColor.Yellow;
        
        DrawHorizontalLine(shiftX, shiftY, longestItemLenght, true);
        for (var i = 0; i < items.Count; i++)
        {
            // Left border + item
            Console.SetCursorPosition(shiftX, shiftY + i + 1);
            Console.Write(itemsToDisplay[i]);
            
            Console.SetCursorPosition(shiftX + longestItemLenght - 1, shiftY + i + 1);
            Console.Write("║");
        }
        
        // Asking user
        var userErrorData = new OnUserErrorData()
        {
            X = shiftX,
            Y = shiftY + menuHeight - 2,
            Length = longestItemLenght
        };
        
        DrawQuestionLine(userErrorData);
        
        DrawHorizontalLine(shiftX, shiftY + menuHeight - 1, longestItemLenght, false);

        var displayIndex = EnterNumber
        (
            1,
            itemsToDisplay.Count,
            shiftX + 4,
            shiftY + menuHeight - 2,
            DrawQuestionLine,
            userErrorData
        ) - 1;
        
        return processedItems[displayIndex].OriginalIndex;
    }

    private void DrawQuestionLine(Object data)
    {
        var realData = data as OnUserErrorData;
        
        Console.SetCursorPosition(realData.X, realData.Y);
        Console.Write($"║ >>{ new string(' ', realData.Length - 5) }║");
    }
    
    private void DrawHorizontalLine(int x, int y, int lenght, bool isUpper)
    {
        var leftmostCharacter = isUpper ? "╔" : "╚";
        var rightmostCharacter = isUpper ? "╗" : "╝";
        
        Console.SetCursorPosition(x, y);
        
        Console.Write(leftmostCharacter);

        for (var i = 1; i < lenght - 1; i++)
        {
            Console.Write("═");
        }
        
        Console.Write(rightmostCharacter);
    }

    private void ClearRectangle(int leftX, int topY, int width, int height, ConsoleColor color)
    {
        var bgColorBackup = Console.BackgroundColor;
        
        Console.BackgroundColor = color;
        for (var y = 0; y < height; y++)
        {
            Console.SetCursorPosition(leftX, topY + y);
            for (var x = 0; x < width; x++)
            {
                Console.Write(" ");
            }
        }

        Console.BackgroundColor = bgColorBackup;
    }

    private int EnterNumber
    (
        int minAllowedNumber,
        int maxAllowedNumber,
        int curX,
        int curY,
        OnUserErrorDelegate onUserError,
        Object errorData
    )
    {
        while (true)
        {
            Console.SetCursorPosition(curX, curY);
            var isSuccess = int.TryParse(Console.ReadLine(), out int result);

            isSuccess = isSuccess && (result >= minAllowedNumber && result <= maxAllowedNumber);
            
            if (isSuccess)
            {
                return result;
            }
            
            // Error processing
            if (onUserError != null)
            {
                onUserError(errorData);
            }
        }
    }

    private class OnUserErrorData
    {
        public int X { get; set; }

        public int Y { get; set; }

        public int Length { get; set; }
    }

    private class MenuItem
    {
        public int OriginalIndex { get; set; }
        
        public string Text { get; set; }
    }
}