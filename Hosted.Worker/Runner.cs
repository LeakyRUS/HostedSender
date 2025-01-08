using Handlers.Abstractions;
using Handlers.Abstractions.Box;
using Handlers.Abstractions.Config;
using Handlers.Abstractions.Osc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Hosted.Worker;

public class Runner : IDisposable
{
    private GeneralSetting? _config;
    private IEnumerable<IBoxHandler> _handlers = [];

    private FileWatcher _watcher;
    private IConfigReader _configReader;
    private readonly IDefaultConfigCreator _defaultConfigCreator;
    private readonly IBoxHandlerBuilder _boxHandlerBuilder;
    private readonly ILogger _logger;
    private readonly string _configPath;
    private readonly CancellationTokenSource _cancellationTokenSource;

    public Runner(
        IDefaultConfigCreator defaultConfigCreator,
        IConfigReader configReader,
        IConfiguration configuration,
        IBoxHandlerBuilder boxHandlerBuilder,
        ILogger<Runner> logger)
    {
        _logger = logger;
        _configReader = configReader;
        _defaultConfigCreator = defaultConfigCreator;
        _boxHandlerBuilder = boxHandlerBuilder;
        _configPath = Path.Combine(Directory.GetCurrentDirectory(), configuration["trackFileName"] ?? "default.json");
        _cancellationTokenSource = new CancellationTokenSource();

        ReadOrCreateConfigIfNotExist().GetAwaiter().GetResult();

        _watcher = new FileWatcher(_configPath, _defaultConfigCreator);
        _watcher.OnChanged += (o, e) => { OnFileChange(o, e).GetAwaiter().GetResult(); };
    }

    public async Task Run(CancellationToken cancellationToken = default)
    {
        RecreateBoxes();

        while (!cancellationToken.IsCancellationRequested && _config != null)
        {
            await Task.Delay(10000, cancellationToken);
        }

        _cancellationTokenSource.Cancel();
        DisposeBoxes();
    }

    private async Task OnFileChange(object sender, FileSystemEventArgs e)
    {
        await ReadOrCreateConfigIfNotExist();
        _logger.LogInformation("File {0} has been changed", e.Name);

        RecreateBoxes();
    }

    private async Task ReadOrCreateConfigIfNotExist()
    {
        _config = await _configReader.ReadOrCreate(_configPath);
    }

    private void RecreateBoxes()
    {
        DisposeBoxes();

        _handlers = _boxHandlerBuilder.BuildCollection(_config!);
        foreach (var handler in _handlers)
        {
            handler.Run(_cancellationTokenSource.Token);
        }
    }

    private void DisposeBoxes()
    {
        foreach (var handler in _handlers)
        {
            (handler as IDisposable)?.Dispose();
            DisposeOscHandlers(handler.OscHandlers);
        }

        _handlers = [];
    }

    private static void DisposeOscHandlers(IEnumerable<IOscHandler> oscHandler)
    {
        foreach (var osc in oscHandler)
        {
            (osc as IDisposable)?.Dispose();
        }
    }

    public void Dispose()
    {
        _cancellationTokenSource.Cancel();
        _watcher.Dispose();
    }
}
