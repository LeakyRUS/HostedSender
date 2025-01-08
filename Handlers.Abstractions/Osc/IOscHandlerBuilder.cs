namespace Handlers.Abstractions.Osc;

public interface IOscHandlerBuilder
{
    IOscHandlerCollection Build(IEnumerable<OscSetting> oscSettings);
}