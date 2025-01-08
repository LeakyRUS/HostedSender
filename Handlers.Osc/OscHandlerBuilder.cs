using Handlers.Abstractions.Osc;
using Microsoft.Extensions.Logging;

namespace Handlers.Osc;

internal class OscHandlerBuilder(ILogger<OscHandler> oscHandlerLogger) : IOscHandlerBuilder
{
    public IOscHandlerCollection Build(IEnumerable<OscSetting> oscSettings)
    {
        var result = new Dictionary<string, IOscHandler>();

        foreach (var oscSetting in oscSettings)
        {
            if (result.ContainsKey(oscSetting.Key))
                continue;

            result[oscSetting.Key] = new OscHandler(oscSetting, oscHandlerLogger);
        }

        return new OscHandlerCollection(result);
    }
}
