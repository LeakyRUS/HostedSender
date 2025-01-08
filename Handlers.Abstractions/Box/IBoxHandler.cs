using Handlers.Abstractions.Osc;

namespace Handlers.Abstractions.Box;

public interface IBoxHandler
{
    IEnumerable<IOscHandler> OscHandlers { get; }
    void Run(CancellationToken cancellationToken = default);
}