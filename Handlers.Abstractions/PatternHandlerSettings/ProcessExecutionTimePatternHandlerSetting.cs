using System.Text.Json.Serialization;

namespace Handlers.Abstractions.PatternHandlerSettings;

[method: JsonConstructor]
public record ProcessExecutionTimePatternHandlerSetting(
    string ProcessName,
    string? Format = null,
    string? TextFormat = null)
    : PatternHandlerSetting(TextFormat)
{
    public string Format { get; } = Format ?? "{0} uptime: {1:hh\\:mm\\:ss}";
}