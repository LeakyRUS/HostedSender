namespace Handlers.Abstractions.PatternCollections;

public interface IPatternCollectionHandler
{
    int FrameCount { get; }
    int FrameDelayMills { get; }

    Task<string> GetResult();
}