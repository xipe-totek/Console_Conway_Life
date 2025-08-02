namespace ConsoleConwayLife.Services.Abstractions;

/// <summary>
/// Interface to work with menus
/// </summary>
public interface IMenu
{
    /// <summary>
    /// Show menu with lines from items and returns index of selected item
    /// </summary>
    int ShowMenuAndReturnUserChoice(IReadOnlyCollection<string> items);
}