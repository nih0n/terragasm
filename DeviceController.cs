using System.Threading;
using System.Threading.Tasks;
using Buttplug.Client;
using Buttplug.Client.Connectors.WebsocketConnector;

namespace Terragasm;

public static class DeviceController
{
    private static readonly ButtplugClient _client = new("Terragasm");
    private static readonly ButtplugWebsocketConnector _connector = new(new("ws://localhost:12345"));

    public static Task ConnectAsync(CancellationToken cancellationToken = default)
        => _client.ConnectAsync(_connector, cancellationToken);

    public static Task DisconnectAsync()
        => _client.DisconnectAsync();

    public static Task VibrateAsync(double speed, int delay)
        => Parallel.ForEachAsync(_client.Devices, async (device, token) =>
            {
                await device.VibrateAsync(speed).ConfigureAwait(false);

                await Task.Delay(delay, token).ConfigureAwait(false);

                await device.VibrateAsync(0).ConfigureAwait(false);
            });
}