using Handlers.Abstractions.Json;
using Handlers.Abstractions.Osc;
using Handlers.Osc;
using Microsoft.Extensions.DependencyInjection;

namespace Handlers.Json;

public static class DependencyInjection
{
    public static IServiceCollection AddHandlersOsc(this IServiceCollection services)
    {
        services.AddTransient<IOscHandlerBuilder, OscHandlerBuilder>();

        return services;
    }
}
