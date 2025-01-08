using Handlers.Abstractions.Json;
using System.Text.Json;

namespace Handlers.Json;

internal class SerializerOptionProvider(IPatternSettingTypeResolver typeResolver) : ISerializerOptionProvider
{
    private JsonSerializerOptions Make()
    {
        var options = new JsonSerializerOptions(JsonSerializerDefaults.General);
        options.TypeInfoResolverChain.Add(typeResolver);
        options.WriteIndented = true;
        options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;

        return options;
    }

    public JsonSerializerOptions Option => Make();
}
