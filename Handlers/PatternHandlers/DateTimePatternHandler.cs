using Handlers.Abstractions.PatternHandlerSettings;
using System.Globalization;

namespace Handlers.PatternHandlers;

internal class DateTimePatternHandler(DateTimePatternHandlerSetting setting)
    : PatternHandler<DateTimePatternHandlerSetting>(setting)
{
    protected override Task<string> GetInnerResult()
    {
        var ci = new CultureInfo(_setting.Culture);
        return Task.FromResult(DateTime.Now.ToString(_setting.DateTimeFormat, ci));
    }
}
