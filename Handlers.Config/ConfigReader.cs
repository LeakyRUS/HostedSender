using Handlers.Abstractions;
using Handlers.Abstractions.Config;
using Handlers.Abstractions.Json;
using System.Text.Json;

namespace Handlers.Config;

public class ConfigReader(IDefaultConfigCreator configCreator, ISerializerOptionProvider provider) : IConfigReader
{
    public async Task<GeneralSetting?> ReadOrCreate(string filePath)
    {
        if (!File.Exists(filePath))
        {
            await CreateFile(filePath);
        }

        using var file = File.OpenRead(filePath);
        var result = await JsonSerializer.DeserializeAsync<GeneralSetting>(file, provider.Option);

        return result;
    }

    private Task CreateFile(string filePath)
    {
        return configCreator.CreateIfNotExist(filePath);
    }
}
