namespace Handlers.Abstractions.Media;

public record MediaInfo(string Track, string Author, TimeSpan Seek, TimeSpan Duration, PlaybackStatus PlaybackStatus);
