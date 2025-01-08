using Handlers.Abstractions.Media;
using Handlers.Abstractions.PatternHandlerSettings;
using Handlers.Abstractions.UserInputInfo;

namespace Handlers.PatternHandlers;

internal class PatternBuilder(Lazy<IMediaInfoProvider> mediaInfoProvider, Lazy<IUserInputProvider> userInputProvider) : IPatternBuilder
{
    private readonly Lazy<IMediaInfoProvider> _mediaInfoProvider = mediaInfoProvider;
    private readonly Lazy<IUserInputProvider> _userInputProvider = userInputProvider;

    public IPatternHandler Build<T>(T setting) where T : PatternHandlerSetting =>
        setting switch
        {
            AfkPatternHandlerSettings set => new AfkPatternHandler(set, _userInputProvider.Value),
            AnimationPatternHandlerSetting set => new AnimationPatternHandler(set),
            DateTimePatternHandlerSetting set => new DateTimePatternHandler(set),
            MediaPatternHandlerSetting set => new MediaPatternHandler(set, _mediaInfoProvider.Value),
            ProcessExecutionTimePatternHandlerSetting set => new ProcessExecutionTimePatternHandler(set),
            _ => new DefaultPatternHandler(new DefaultPatternHandlerSetting())
        };
}
