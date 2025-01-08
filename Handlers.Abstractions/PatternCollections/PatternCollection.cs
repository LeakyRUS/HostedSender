using Handlers.Abstractions.PatternHandlerSettings;
using System.Text.Json.Serialization;

namespace Handlers.Abstractions.PatternCollections;

[method: JsonConstructor]
public record PatternCollection(string TextFormat, int FrameDelayMills, int Frames, IList<PatternHandlerSetting> Settings);
