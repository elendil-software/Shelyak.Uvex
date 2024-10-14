using FastEndpoints;
using Microsoft.Extensions.Options;
using Shelyak.Usis;
using Shelyak.Uvex.Alpaca;

namespace Shelyak.Uvex.Web.Endpoints.Config.ComPort;

public class SetComPortEndpoint : Endpoint<SetComPortRequest>
{
    internal const string RoutePattern = "/" + ApiRoutes.ConfigPort;
    
    private readonly ISettingsUpdater _settingsUpdater;
    private IOptionsSnapshot<SerialPortSettings> SerialPortSettingsOptions { get; set; }

    public SetComPortEndpoint(ISettingsUpdater settingsUpdater, IOptionsSnapshot<SerialPortSettings> serialPortSettingsOptions)
    {
        _settingsUpdater = settingsUpdater;
        SerialPortSettingsOptions = serialPortSettingsOptions;
    }
    
    public override void Configure()
    {
        Put(RoutePattern);
        Group<ConfigGroup>();
        Version(1);
        AllowAnonymous();
    }

    public override async Task HandleAsync(SetComPortRequest req, CancellationToken ct)
    {
        var serialPortSettings = SerialPortSettingsOptions.Value;
        serialPortSettings.PortName = req.PortName;
        await _settingsUpdater.UpdateSerialPort(serialPortSettings);
        await SendOkAsync(ct);
    }
}