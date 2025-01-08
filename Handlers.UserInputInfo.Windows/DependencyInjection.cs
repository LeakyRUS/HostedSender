using Handlers.Abstractions.UserInputInfo;
using Handlers.UserInputInfo.Windows;
using Microsoft.Extensions.DependencyInjection;

namespace Handlers.Json;

public static class DependencyInjection
{
    public static IServiceCollection AddUserInputProvider(this IServiceCollection services)
    {
        services.AddTransient<IUserInputProvider, UserInputProvider>();

        return services;
    }
}
