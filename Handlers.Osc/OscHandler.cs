using CoreOSC;
using CoreOSC.IO;
using Handlers.Abstractions.Osc;
using Microsoft.Extensions.Logging;
using System.Net.Sockets;

namespace Handlers.Osc;

public class OscHandler : IDisposable, IOscHandler
{
    private readonly string _oscUrl;
    private readonly string _key;
    private readonly UdpClient _udpClient;
    private readonly ILogger _logger;

    public OscHandler(OscSetting setting, ILogger<OscHandler> logger)
    {
        _key = setting.Key;
        _oscUrl = setting.OscUrl;
        _logger = logger;

        _udpClient = new UdpClient(setting.BaseUrl, setting.BasePort);
    }

    public string Key => _key;

    public Task SendMessage(string text, CancellationToken cancellationToken = default)
    {
        var oscArgs = new object[]
        {
            text,
            OscTrue.True,
            OscFalse.False
        };

        _logger.LogDebug("Sending OSC message {0} \"{1}\" => {2}",
            _oscUrl,
            text,
            string.Join(" ", oscArgs.Select(x => x?.ToString()?.Replace("\n", "\\n") ?? string.Empty)));

        var message = new OscMessage(new Address(_oscUrl), oscArgs);

        return _udpClient.SendMessageAsync(message);
    }

    public void Dispose()
    {
        _udpClient.Dispose();
    }
}
