using ConsoleConwayLife.Models;

namespace ConsoleConwayLife.Services.Abstractions;

/// <summary>
/// Loads all rules from directory
/// </summary>
public interface IRulesetsLoader
{
    /// <summary>
    /// Load ruleset from file
    /// </summary>
    Ruleset LoadRuleset(string path);

    /// <summary>
    /// Load all rulesets from directory
    /// </summary>
    IReadOnlyCollection<RulesetsMenuItem> LoadRulesetsFromDirectory(string path);
}