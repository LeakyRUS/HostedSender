namespace Handlers.Abstractions.PatternHandlerSettings;

public interface IPatternBuilder
{
    IPatternHandler Build<T>(T setting) where T : PatternHandlerSetting;
}
