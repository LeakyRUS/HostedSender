using Handlers.Abstractions.Json;
using Handlers.Abstractions.PatternHandlerSettings;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

namespace Handlers.Json;

internal class PatternSettingTypeResolver : DefaultJsonTypeInfoResolver, IPatternSettingTypeResolver
{
    public override JsonTypeInfo GetTypeInfo(Type type, JsonSerializerOptions options)
    {
        var jsonTypeInfo = base.GetTypeInfo(type, options);

        var baseSettingType = typeof(PatternHandlerSetting);
        if (jsonTypeInfo.Type == baseSettingType)
        {
            jsonTypeInfo.PolymorphismOptions = new JsonPolymorphismOptions
            {
                DerivedTypes =
                {
                    new JsonDerivedType(typeof(AfkPatternHandlerSettings), "afk"),
                    new JsonDerivedType(typeof(AnimationPatternHandlerSetting), "anim"),
                    new JsonDerivedType(typeof(DateTimePatternHandlerSetting), "dt"),
                    new JsonDerivedType(typeof(MediaPatternHandlerSetting), "media"),
                    new JsonDerivedType(typeof(ProcessExecutionTimePatternHandlerSetting), "pid")
                }
            };
        }

        return jsonTypeInfo;
    }
}
