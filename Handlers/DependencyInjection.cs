using Handlers.Abstractions.PatternHandlerSettings;
using Handlers.PatternHandlers;
using Microsoft.Extensions.DependencyInjection;

namespace Handlers;

public static class DependencyInjection
{
    public static IServiceCollection AddHandlers(this IServiceCollection services)
    {
        services.AddTransient<IPatternBuilder, PatternBuilder>();

        return services;
    }
}
