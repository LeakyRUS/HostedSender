using System.Text.Json.Serialization;

namespace Handlers.Abstractions.PatternHandlerSettings;

[method: JsonConstructor]
public record AfkPatternHandlerSettings(
    int TimeSeconds,
    string? Message = null,
    string? TextFormat = "{0}")
    : PatternHandlerSetting(TextFormat)
{
    public string Message { get; } = Message ?? "AFK\n";
}