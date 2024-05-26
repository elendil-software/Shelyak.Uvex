using FastEndpoints;
using Microsoft.Extensions.Options;
using Shelyak.Usis;
using Shelyak.Uvex.Alpaca;
using Shelyak.Uvex.Web.Core.Settings;

namespace Shelyak.Uvex.Web.Endpoints.Config;

public class SetPortEndpoint : Endpoint<SetPortRequest>
{
    private readonly ISettingsUpdater _settingsUpdater;
    private IOptionsSnapshot<SerialPortSettings> SerialPortSettingsOptions { get; set; }

    public SetPortEndpoint(ISettingsUpdater settingsUpdater, IOptionsSnapshot<SerialPortSettings> serialPortSettingsOptions)
    {
        _settingsUpdater = settingsUpdater;
        SerialPortSettingsOptions = serialPortSettingsOptions;
    }
    
    public override void Configure()
    {
        Put("/" + ApiRoutes.ConfigPort);
        Group<ConfigGroup>();
        Version(1);
        AllowAnonymous();
    }

    public override async Task HandleAsync(SetPortRequest req, CancellationToken ct)
    {
        var port = SerialPortSettingsOptions.Value.PortName; var serialPortSettings = SerialPortSettingsOptions.Value;
        serialPortSettings.PortName = req.PortName;
        await _settingsUpdater.UpdateSerialPort(serialPortSettings);
        await SendOkAsync(ct);
    }
}