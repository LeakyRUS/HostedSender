using Handlers.Abstractions.Osc;
using System.Collections;
using System.Collections.Frozen;
using System.Diagnostics.CodeAnalysis;

namespace Handlers.Osc;

internal class OscHandlerCollection : IOscHandlerCollection
{
    private readonly HashSet<string> _usedKeys = [];
    private readonly FrozenDictionary<string, IOscHandler> _innerDictionary;

    public OscHandlerCollection(IReadOnlyDictionary<string, IOscHandler> dictionary)
    {
        _innerDictionary = dictionary.ToFrozenDictionary();
    }

    public bool IsKeyUsed(string key) => _usedKeys.Contains(key);

    public IOscHandler this[string key]
    {
        get
        {
            var result = _innerDictionary[key];
            _usedKeys.Add(key);
            return result;
        }
    }

    public IEnumerable<string> Keys => _innerDictionary.Keys;

    public int Count => _innerDictionary.Count;

    public IEnumerable<IOscHandler> Values => throw new NotImplementedException();

    public bool ContainsKey(string key)
    {
        return _innerDictionary.ContainsKey(key);
    }

    public bool TryGetValue(string key, [MaybeNullWhen(false)] out IOscHandler value)
    {
        var result = _innerDictionary.TryGetValue(key, out value);
        if (result == true)
        {
            _usedKeys.Add(key);
        }
        return result;
    }

    public IOscHandler? GetValueOrDefault(string key)
    {
        if (TryGetValue(key, out var value))
            return value;

        return null;
    }

    public IEnumerator<KeyValuePair<string, IOscHandler>> GetEnumerator()
    {
        return _innerDictionary.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return _innerDictionary.GetEnumerator();
    }

    public void Dispose()
    {
        var notUsed = _innerDictionary.Keys.Except(_usedKeys);

        foreach (var item in notUsed)
        {
            (_innerDictionary[item] as IDisposable)?.Dispose();
        }
    }
}
