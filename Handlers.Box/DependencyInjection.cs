using Handlers.Abstractions.Box;
using Microsoft.Extensions.DependencyInjection;

namespace Handlers.Box;

public static class DependencyInjection
{
    public static IServiceCollection AddHandlersBox(this IServiceCollection services)
    {
        services.AddTransient<IBoxHandlerBuilder, BoxHandlerBuilder>();

        return services;
    }
}
