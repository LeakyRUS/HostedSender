using Handlers.Abstractions;
using Handlers.Abstractions.Box;
using Handlers.Abstractions.Osc;
using Handlers.Abstractions.PatternCollections;
using Microsoft.Extensions.Logging;

namespace Handlers.Box;

internal class BoxHandlerBuilder(
    ILogger<BoxHandler> boxHandlerLogger,
    IPatternCollectionHandlerBuilder patternCollectionHandlerBuilder,
    IOscHandlerBuilder oscHandlerBuilder)
    : IBoxHandlerBuilder
{
    public IBoxHandler Build(
        IReadOnlyDictionary<string, IOscHandler> handlers,
        BoxSetting boxSetting,
        IList<IPatternCollectionHandler> patternCollection)
    {
        return new BoxHandler(handlers, boxSetting, patternCollection, boxHandlerLogger);
    }

    public IEnumerable<IBoxHandler> BuildCollection(GeneralSetting generalSetting)
    {
        var result = new List<IBoxHandler>();

        using var oscCollection = oscHandlerBuilder.Build(generalSetting.OscSettings);

        foreach (var boxSetting in generalSetting.BoxSettings)
        {
            var patternCollection = patternCollectionHandlerBuilder.Build(boxSetting.PatternCollections);
            result.Add(Build(oscCollection, boxSetting, patternCollection));
        }

        return result;
    }
}
