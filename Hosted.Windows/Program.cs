using Hosted.Worker.Windows;
using Microsoft.Extensions.Hosting;

namespace Hosted.Windows;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = Host.CreateApplicationBuilder(args);
        builder.Services.AddWorkerDependencies();

        var host = builder.Build();
        host.Run();
    }
}