using System.Text.Json.Serialization;

namespace Handlers.Abstractions.PatternHandlerSettings;

[method: JsonConstructor]
public record AnimationPatternHandlerSetting(IList<AnimationInfo> AnimationInfos, string? TextFormat = null)
    : PatternHandlerSetting(TextFormat);
