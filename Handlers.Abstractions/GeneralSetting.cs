using Handlers.Abstractions.Box;
using Handlers.Abstractions.Osc;
using System.Text.Json.Serialization;

namespace Handlers.Abstractions;

[method: JsonConstructor]
public record GeneralSetting(IEnumerable<OscSetting> OscSettings, IEnumerable<BoxSetting> BoxSettings);
