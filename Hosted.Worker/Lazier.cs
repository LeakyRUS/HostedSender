using Microsoft.Extensions.DependencyInjection;

namespace Hosted.Worker;

internal class Lazier<T> : Lazy<T> where T : class
{
    public Lazier(IServiceProvider serviceProvider)
        : base(serviceProvider.GetRequiredService<T>)
    {
    }
}
