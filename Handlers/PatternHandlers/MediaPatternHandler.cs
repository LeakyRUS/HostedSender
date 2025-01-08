using Handlers.Abstractions.Media;
using Handlers.Abstractions.PatternHandlerSettings;

namespace Handlers.PatternHandlers;

internal class MediaPatternHandler(MediaPatternHandlerSetting setting,
    IMediaInfoProvider mediaProvider)
    : PatternHandler<MediaPatternHandlerSetting>(setting)
{
    protected override async Task<string> GetInnerResult()
    {
        var mediaInfo = await mediaProvider.GetMediaInfoAsync();
        if (mediaInfo == null)
            return _setting.DefaultIfNoMedia;

        var playback = mediaInfo.PlaybackStatus.ToString();
        playback = _setting.StatusSettings.GetValueOrDefault(playback) ?? playback;

        var atFormatted = string.Format(_setting.AuthorTitleFormat, playback, mediaInfo.Author, mediaInfo.Track);

        var tFormatted = _setting.ShowTime && !mediaInfo.Duration.Equals(TimeSpan.Zero)
            ? string.Format(_setting.TimeFormat, mediaInfo.Seek, mediaInfo.Duration)
            : string.Empty;

        return string.Format(_setting.Format, atFormatted, tFormatted);
    }
}
