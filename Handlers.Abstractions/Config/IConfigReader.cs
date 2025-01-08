namespace Handlers.Abstractions.Config;

public interface IConfigReader
{
    Task<GeneralSetting?> ReadOrCreate(string filePath);
}