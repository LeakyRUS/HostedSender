namespace Handlers.Abstractions.Osc;

public interface IOscHandler
{
    string Key { get; }

    Task SendMessage(string text, CancellationToken cancellationToken = default);
}