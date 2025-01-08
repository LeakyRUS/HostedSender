using Handlers.Abstractions.PatternCollections;
using Handlers.PatternCollections;
using Microsoft.Extensions.DependencyInjection;

namespace Handlers.Json;

public static class DependencyInjection
{
    public static IServiceCollection AddHandlersPatternCollections(this IServiceCollection services)
    {
        services.AddTransient<IPatternCollectionHandlerBuilder, PatternCollectionHandlerBuilder>();

        return services;
    }
}
