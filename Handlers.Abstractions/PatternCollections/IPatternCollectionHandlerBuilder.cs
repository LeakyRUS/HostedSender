namespace Handlers.Abstractions.PatternCollections;

public interface IPatternCollectionHandlerBuilder
{
    IList<IPatternCollectionHandler> Build(IList<PatternCollection> patternCollections);
}