using System.Text.Json.Serialization;

namespace ConsoleConwayLife.Models;

/// <summary>
/// Ruleset for game
/// </summary>
public class Ruleset
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("birthRule")]
    public List<int> BirthRule { get; set; }
    
    [JsonPropertyName("surviveRule")]
    public List<int> SurviveRule { get; set; }
}