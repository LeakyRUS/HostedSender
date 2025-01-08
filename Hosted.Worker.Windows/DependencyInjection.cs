using Handlers.Json;
using Microsoft.Extensions.DependencyInjection;

namespace Hosted.Worker.Windows;

public static class DependencyInjection
{
    public static IServiceCollection AddWorkerDependencies(this IServiceCollection services)
    {
        services.AddHostedWorker();
        services.AddHandlersMedia();
        services.AddUserInputProvider();

        return services;
    }
}
