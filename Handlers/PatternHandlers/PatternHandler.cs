using Handlers.Abstractions.PatternHandlerSettings;

namespace Handlers.PatternHandlers;

internal abstract class PatternHandler<T>(T setting) : IPatternHandler
    where T : PatternHandlerSetting
{
    protected T _setting => setting;

    protected abstract Task<string> GetInnerResult();

    public async Task<string> GetResult()
    {
        SetupMissingException.ThrowIfNull(_setting, typeof(T));

        var result = await GetInnerResult();

        return string.Format(_setting.TextFormat, result);
    }
}
