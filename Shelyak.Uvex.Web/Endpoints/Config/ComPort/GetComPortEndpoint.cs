using FastEndpoints;
using Microsoft.Extensions.Options;
using Shelyak.Usis;
using Shelyak.Uvex.Alpaca;

namespace Shelyak.Uvex.Web.Endpoints.Config.ComPort;

public class GetComPortEndpoint : EndpointWithoutRequest<string>
{
    private IOptionsSnapshot<SerialPortSettings> SerialPortSettingsOptions { get; set; }

    public GetComPortEndpoint(ISettingsUpdater settingsUpdater, IOptionsSnapshot<SerialPortSettings> serialPortSettingsOptions)
    {
        SerialPortSettingsOptions = serialPortSettingsOptions;
    }
    
    public override void Configure()
    {
        Get("/" + ApiRoutes.ConfigPort);
        Group<ConfigGroup>();
        Version(1);
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var port = SerialPortSettingsOptions.Value.PortName;
        await SendAsync(port, cancellation: ct);
    }
}