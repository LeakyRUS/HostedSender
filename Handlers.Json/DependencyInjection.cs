using Handlers.Abstractions.Json;
using Microsoft.Extensions.DependencyInjection;

namespace Handlers.Json;

public static class DependencyInjection
{
    public static IServiceCollection AddHandlersJson(this IServiceCollection services)
    {
        services.AddSingleton<IPatternSettingTypeResolver, PatternSettingTypeResolver>();
        services.AddScoped<ISerializerOptionProvider, SerializerOptionProvider>();

        return services;
    }
}
