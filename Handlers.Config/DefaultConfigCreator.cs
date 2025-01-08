using Handlers.Abstractions;
using Handlers.Abstractions.Box;
using Handlers.Abstractions.Config;
using Handlers.Abstractions.Json;
using Handlers.Abstractions.Osc;
using Handlers.Abstractions.PatternCollections;
using Handlers.Abstractions.PatternHandlerSettings;
using System.Text.Json;

namespace Handlers.Config;

internal class DefaultConfigCreator(ISerializerOptionProvider provider) : IDefaultConfigCreator
{
    public async Task CreateCopy(string originalFilePath, string newFilePath)
    {
        if (originalFilePath == newFilePath)
            return;

        using var source = File.Open(originalFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite | FileShare.Delete);
        using var desctignation = File.Open(newFilePath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
        await source.CopyToAsync(desctignation);
    }

    public async Task CreateIfNotExist(string filePath)
    {
        var afk = new AfkPatternHandlerSettings(30, "AFK\n");
        var anim = new AnimationPatternHandlerSetting([new AnimationInfo("Test01", 1), new AnimationInfo("Test02", 2), new AnimationInfo("Test03", 1)]);
        var dt = new DateTimePatternHandlerSetting("t");
        var media = new MediaPatternHandlerSetting(true);
        var pid = new ProcessExecutionTimePatternHandlerSetting("vrchat");

        var pc = new PatternCollection("{0}{1}{2}{3}{4}", 3000, 3, [afk, anim, dt, media, pid]);
        var osc = new OscSetting("chatbox", "/chatbox/input", "127.0.0.1", 9000);
        var box = new BoxSetting(["chatbox"], [pc, new PatternCollection("Опа! 3 секунды", 3000, 1, [])]);
        var gen = new GeneralSetting([osc], [box]);

        using var newFile = File.Create(filePath);
        await JsonSerializer.SerializeAsync(newFile, gen, provider.Option);
    }
}
