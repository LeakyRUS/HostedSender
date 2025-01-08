using Handlers.Abstractions.Config;
using Microsoft.Extensions.DependencyInjection;

namespace Handlers.Config;

public static class DependencyInjection
{
    public static IServiceCollection AddHandlersConfig(this IServiceCollection services)
    {
        services.AddSingleton<IDefaultConfigCreator, DefaultConfigCreator>();
        services.AddTransient<IConfigReader, ConfigReader>();

        return services;
    }
}
