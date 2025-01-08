namespace Handlers.Abstractions.Media;

public interface IMediaInfoProvider
{
    Task<MediaInfo?> GetMediaInfoAsync();
}
