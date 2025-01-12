using System.Collections.Frozen;
using System.Text.Json.Serialization;

namespace Handlers.Abstractions.PatternHandlerSettings;

[method: JsonConstructor]
public record MediaPatternHandlerSetting(
    bool ShowTime,
    IReadOnlyDictionary<string, string>? StatusSettings = null,
    string? DefaultIfNoMedia = null,
    string? Format = null,
    string? AuthorTitleFormat = null,
    string? TimeFormat = null,
    string? TextFormat = "{0}") : PatternHandlerSetting(TextFormat)
{
    public IReadOnlyDictionary<string, string> StatusSettings { get; } =
        StatusSettings?.Any() ?? false
        ? StatusSettings.ToFrozenDictionary()
        : new Dictionary<string, string>
        {
            ["Closed"] = "🗙",
            ["Opened"] = "📂",
            ["Changing"] = "⏭",
            ["Stopped"] = "⏹",
            ["Playing"] = "▶️",
            ["Paused"] = "⏸"
        };

    public string DefaultIfNoMedia { get; } = DefaultIfNoMedia ?? string.Empty;

    public string Format { get; } = Format ?? "{0}\n{1}";

    public string AuthorTitleFormat { get; } = AuthorTitleFormat ?? "{0}{1} - {2}";

    public string TimeFormat { get; } = TimeFormat ?? "{0:mm\\:ss} - {1:mm\\:ss}\n";
}