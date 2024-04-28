using FastEndpoints;
using Microsoft.Extensions.Options;
using Shelyak.Usis;
using Shelyak.Uvex.Alpaca;
using Shelyak.Uvex.Web.Core.Settings;

namespace Shelyak.Uvex.Web.Endpoints.Config;

public class GetPortEndpoint : EndpointWithoutRequest<string>
{
    private readonly ISerialPortSettingsWriter _serialPortSettingsWriter;
    private IOptionsSnapshot<SerialPortSettings> SerialPortSettingsOptions { get; set; }

    public GetPortEndpoint(ISerialPortSettingsWriter serialPortSettingsWriter, IOptionsSnapshot<SerialPortSettings> serialPortSettingsOptions)
    {
        _serialPortSettingsWriter = serialPortSettingsWriter;
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