using Handlers.Abstractions.PatternHandlerSettings;

namespace Handlers.PatternHandlers;

internal class DefaultPatternHandler(DefaultPatternHandlerSetting setting)
    : PatternHandler<DefaultPatternHandlerSetting>(setting)
{
    protected override Task<string> GetInnerResult()
    {
        return Task.FromResult(string.Empty);
    }
}
