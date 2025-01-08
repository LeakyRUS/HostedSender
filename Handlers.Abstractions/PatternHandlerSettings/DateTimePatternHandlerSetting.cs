using System.Text.Json.Serialization;

namespace Handlers.Abstractions.PatternHandlerSettings;

[method: JsonConstructor]
public record DateTimePatternHandlerSetting(
    string DateTimeFormat,
    string? Culture = null,
    string? TextFormat = null)
    : PatternHandlerSetting(TextFormat)
{
    public string Culture { get; } = Culture ?? "en-US";
}
