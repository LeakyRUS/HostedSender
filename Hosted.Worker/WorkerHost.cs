using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Hosted.Worker;

public class WorkerHost(IServiceScopeFactory scopeFactory) : IHostedService
{
    private Thread? _workingThread;
    private CancellationTokenSource? _cancellationTokenSource;

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _cancellationTokenSource = new CancellationTokenSource();

        _workingThread = new(new ParameterizedThreadStart(ThreadRun));
        _workingThread.IsBackground = false;
        _workingThread.Start(_cancellationTokenSource.Token);

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _cancellationTokenSource?.Cancel();

        return Task.CompletedTask;
    }

    private void ThreadRun(object? obj)
    {
        if (obj == null)
            return;

        var ct = (CancellationToken)obj;

        using (var scope = scopeFactory.CreateScope())
        {
            var runner = scope.ServiceProvider.GetRequiredService<Runner>();

            runner.Run(ct).GetAwaiter().GetResult();
        }
    }
}
