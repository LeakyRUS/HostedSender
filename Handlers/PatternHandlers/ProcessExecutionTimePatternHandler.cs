using Handlers.Abstractions.PatternHandlerSettings;
using System.Diagnostics;

namespace Handlers.PatternHandlers;

internal class ProcessExecutionTimePatternHandler(ProcessExecutionTimePatternHandlerSetting setting)
    : PatternHandler<ProcessExecutionTimePatternHandlerSetting>(setting)
{
    protected override Task<string> GetInnerResult()
    {
        var result = string.Empty;

        var process = Process
            .GetProcessesByName(_setting.ProcessName)
            .FirstOrDefault();

        if (process != null)
            result = string.Format(_setting.Format, process.ProcessName, DateTime.Now - process.StartTime);

        return Task.FromResult(result);
    }
}
