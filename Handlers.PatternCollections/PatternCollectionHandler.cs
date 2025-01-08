using Handlers.Abstractions.PatternCollections;
using Handlers.Abstractions.PatternHandlerSettings;

namespace Handlers.PatternCollections;

internal class PatternCollectionHandler : IPatternCollectionHandler
{
    private int _frameCount;
    private int _frameDelayMills;
    private string _textFormat;
    private List<IPatternHandler> _patternHandlers = [];

    public int FrameCount => _frameCount;
    public int FrameDelayMills => _frameDelayMills;

    public PatternCollectionHandler(PatternCollection setting, IPatternBuilder patternBuilder)
    {
        _frameCount = setting.Frames;
        _frameDelayMills = setting.FrameDelayMills;
        _textFormat = setting.TextFormat;

        foreach (var pSet in setting.Settings)
        {
            _patternHandlers.Add(patternBuilder.Build(pSet));
        }
    }

    public async Task<string> GetResult()
    {
        var results = await Task.WhenAll(_patternHandlers.Select(x => x.GetResult()));

        return string.Format(_textFormat, results);
    }
}
