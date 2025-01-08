using Handlers.Abstractions.Box;
using Handlers.Abstractions.Osc;
using Handlers.Abstractions.PatternCollections;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Handlers.Box;

internal class BoxHandler : IBoxHandler, IDisposable
{
    private readonly Thread _thread;
    private readonly CancellationTokenSource _internalToken;
    private CancellationTokenSource _joinedToken;
    private readonly List<IOscHandler> _oscHandlers = [];

    private readonly LinkedList<IPatternCollectionHandler> _patternSettingHandler;
    private LinkedListNode<IPatternCollectionHandler> _currentHandler;
    private int _frameNumber = 1;

    private ILogger<BoxHandler> _logger;

    private bool _disposed;

    public BoxHandler(
        IReadOnlyDictionary<string, IOscHandler> handlers,
        BoxSetting boxSetting,
        IList<IPatternCollectionHandler> patternCollection,
        ILogger<BoxHandler> logger)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(handlers.Count, 1, nameof(handlers));
        ArgumentOutOfRangeException.ThrowIfLessThan(patternCollection.Count, 1, nameof(patternCollection));

        _logger = logger;

        foreach (var key in boxSetting.OscSettingKeys)
        {
            var handler = handlers.GetValueOrDefault(key);

            if (handler != null)
                _oscHandlers.Add(handler);
        }

        _patternSettingHandler = new LinkedList<IPatternCollectionHandler>(patternCollection);
        _currentHandler = _patternSettingHandler.First!;

        _internalToken = new CancellationTokenSource();

        _thread = new(new ParameterizedThreadStart(ThreadLoop));
        _thread.IsBackground = true;
    }

    public IEnumerable<IOscHandler> OscHandlers => _oscHandlers;

    public void Run(CancellationToken cancellationToken = default)
    {
        _joinedToken = CancellationTokenSource
            .CreateLinkedTokenSource(cancellationToken, _internalToken.Token);

        _thread.Start(_joinedToken.Token);
    }

    private async Task<string> GetNextFrame()
    {
        if (_currentHandler == null)
            return string.Empty;

        var val = _currentHandler.Value;
        var result = await val.GetResult();

        _frameNumber++;
        if (_frameNumber > val.FrameCount)
        {
            _frameNumber = 1;
            _currentHandler = _currentHandler.Next!;
            if (_currentHandler == null)
            {
                _currentHandler = _patternSettingHandler.First!;
            }
        }

        return result;
    }

    private void ThreadLoop(object? obj)
    {
        if (obj is null)
            return;
        var ct = (CancellationToken)obj;

        try
        {
            ThreadAsyncLoop(ct).GetAwaiter().GetResult();
        }
        catch { }
    }

    private async Task ThreadAsyncLoop(CancellationToken cancellationToken = default)
    {
        var watch = new Stopwatch();

        while (!cancellationToken.IsCancellationRequested)
        {
            watch.Start();

            var delay = _currentHandler.Value.FrameDelayMills;
            var frame = GetNextFrame().GetAwaiter().GetResult();

            _logger.LogDebug("Osc keys: {0}. Sending \"{1}\"",
                string.Join(", ", _oscHandlers.Select(x => x.Key)),
                frame);

            await Task.WhenAll(_oscHandlers.Select(x => x.SendMessage(frame, cancellationToken)));

            watch.Stop();

            var sleepTill = delay - (int)watch.ElapsedMilliseconds; // For debug
            await Task.Delay(sleepTill < 0 ? 0 : sleepTill, cancellationToken);
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_disposed)
            return;

        if (disposing)
        {
            _internalToken.Cancel();
            _joinedToken?.Dispose();
        }

        _disposed = true;
    }
}
