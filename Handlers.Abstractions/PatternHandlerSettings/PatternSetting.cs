using System.Text.Json.Serialization;

namespace Handlers.Abstractions.PatternHandlerSettings;

[method: JsonConstructor]
public abstract record PatternHandlerSetting(string? TextFormat = null)
{
    public string TextFormat { get; } = TextFormat ?? "{0}\n";
}
