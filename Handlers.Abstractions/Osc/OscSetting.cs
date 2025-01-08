using System.Text.Json.Serialization;

namespace Handlers.Abstractions.Osc;

[method: JsonConstructor]
public record OscSetting(string Key, string OscUrl, string BaseUrl, int BasePort);
