using FastEndpoints;
using Microsoft.Extensions.Options;
using Shelyak.Usis;
using Shelyak.Uvex.Alpaca;
using Shelyak.Uvex.Web.Core.Settings;

namespace Shelyak.Uvex.Web.Endpoints.Config;

public class GetPortEndpoint : EndpointWithoutRequest<string>
{
    private readonly ISettingsUpdater _settingsUpdater;
    private IOptionsSnapshot<SerialPortSettings> SerialPortSettingsOptions { get; set; }

    public GetPortEndpoint(ISettingsUpdater settingsUpdater, IOptionsSnapshot<SerialPortSettings> serialPortSettingsOptions)
    {
        _settingsUpdater = settingsUpdater;
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