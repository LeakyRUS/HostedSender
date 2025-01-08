using Handlers.Abstractions.Media;
using Windows.Media.Control;

namespace Handlers.Media.Windows;

internal class MediaInfoProvider : IMediaInfoProvider
{
    private readonly GlobalSystemMediaTransportControlsSessionManager _manager =
        GlobalSystemMediaTransportControlsSessionManager.RequestAsync().GetAwaiter().GetResult();

    public async Task<MediaInfo?> GetMediaInfoAsync()
    {
        var cs = _manager.GetCurrentSession();
        if (cs == null)
            return null;

        var pi = cs.GetPlaybackInfo();
        var tp = cs.GetTimelineProperties();
        var mp = await cs.TryGetMediaPropertiesAsync();

        return new MediaInfo(mp.Title, mp.Artist, tp.Position, tp.EndTime, (PlaybackStatus)(int)pi.PlaybackStatus);
    }
}
