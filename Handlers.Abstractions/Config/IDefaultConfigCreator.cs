namespace Handlers.Abstractions.Config;

public interface IDefaultConfigCreator
{
    Task CreateIfNotExist(string filePath);
    Task CreateCopy(string originalFilePath, string newFilePath);
}
