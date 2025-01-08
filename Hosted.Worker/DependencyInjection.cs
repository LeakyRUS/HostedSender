using Handlers;
using Handlers.Box;
using Handlers.Config;
using Handlers.Json;
using Microsoft.Extensions.DependencyInjection;

namespace Hosted.Worker;

public static class DependencyInjection
{
    public static IServiceCollection AddHostedWorker(this IServiceCollection services)
    {
        services.AddHandlers();
        services.AddHandlersBox();
        services.AddHandlersConfig();
        services.AddHandlersJson();
        services.AddHandlersOsc();
        services.AddHandlersPatternCollections();

        services.AddTransient(typeof(Lazy<>), typeof(Lazier<>));
        services.AddScoped<Runner>();
        services.AddHostedService<WorkerHost>();

        return services;
    }
}
