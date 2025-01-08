using Handlers.Abstractions.PatternCollections;
using Handlers.Abstractions.PatternHandlerSettings;

namespace Handlers.PatternCollections;

internal class PatternCollectionHandlerBuilder(IPatternBuilder patternBuilder) : IPatternCollectionHandlerBuilder
{
    public IList<IPatternCollectionHandler> Build(IList<PatternCollection> patternCollections)
    {
        return patternCollections
            .Select(p => new PatternCollectionHandler(p, patternBuilder) as IPatternCollectionHandler)
            .ToList();
    }
}
