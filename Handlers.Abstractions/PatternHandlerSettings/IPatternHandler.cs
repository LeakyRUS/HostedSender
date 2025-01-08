namespace Handlers.Abstractions.PatternHandlerSettings;

public interface IPatternHandler
{
    Task<string> GetResult();
}
