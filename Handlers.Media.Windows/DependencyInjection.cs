using Handlers.Abstractions.Media;
using Handlers.Media.Windows;
using Microsoft.Extensions.DependencyInjection;

namespace Handlers.Json;

public static class DependencyInjection
{
    public static IServiceCollection AddHandlersMedia(this IServiceCollection services)
    {
        services.AddSingleton<IMediaInfoProvider, MediaInfoProvider>();

        return services;
    }
}
