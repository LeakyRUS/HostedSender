namespace Handlers.Abstractions.Osc;

public interface IOscHandlerCollection : IReadOnlyDictionary<string, IOscHandler>, IDisposable
{
    public bool IsKeyUsed(string key);
}
