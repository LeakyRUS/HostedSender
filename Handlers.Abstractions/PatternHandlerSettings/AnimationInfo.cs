using System.Text.Json.Serialization;

namespace Handlers.Abstractions.PatternHandlerSettings;

[method: JsonConstructor]
public record AnimationInfo(string Text, int FrameCount)
{
    public int FrameCount { get; } = FrameCount;
}
