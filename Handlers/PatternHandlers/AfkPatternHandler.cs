using Handlers.Abstractions.PatternHandlerSettings;
using Handlers.Abstractions.UserInputInfo;

namespace Handlers.PatternHandlers;

internal class AfkPatternHandler(
    AfkPatternHandlerSettings setting,
    IUserInputProvider userInputProvider)
    : PatternHandler<AfkPatternHandlerSettings>(setting)
{
    protected override Task<string> GetInnerResult()
    {
        var result = string.Empty;

        var idle = userInputProvider
            .GetUserInputInfo()
            .IdleTime;

        if (idle.TotalSeconds >= _setting.TimeSeconds)
            result = _setting.Message;

        return Task.FromResult(result);
    }
}
