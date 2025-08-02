namespace ConsoleConwayLife.Models;

public class RulesetsMenuItem
{
    public string Name { get; private set; }

    public string Path { get; private set; }

    public RulesetsMenuItem(string name, string path)
    {
        Name = name;
        Path = path;
    }
}