using Handlers.Abstractions.PatternCollections;
using System.Text.Json.Serialization;

namespace Handlers.Abstractions.Box;

[method: JsonConstructor]
public record BoxSetting(IEnumerable<string> OscSettingKeys, IList<PatternCollection> PatternCollections);
