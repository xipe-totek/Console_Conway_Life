using System.Text.Json;
using ConsoleConwayLife.Models;
using ConsoleConwayLife.Services.Abstractions;

namespace ConsoleConwayLife.Services.Implementations;

public class RulesetsLoader :  IRulesetsLoader
{
    public Ruleset LoadRuleset(string path)
    {
        return JsonSerializer.Deserialize<Ruleset>(File.ReadAllText(path));
    }

    public IReadOnlyCollection<RulesetsMenuItem> LoadRulesetsFromDirectory(string path)
    {
        return Directory
            .GetFiles(path, "*.json")
            .Select(filePath => new Tuple<string, Ruleset>(filePath, LoadRuleset(filePath)))
            .Select(tuple => new RulesetsMenuItem(tuple.Item2.Name, tuple.Item1))
            .ToList();
    }
}