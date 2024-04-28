using System.IO.Ports;
using FastEndpoints;
using Shelyak.Uvex.Alpaca;

namespace Shelyak.Uvex.Web.Endpoints.Config;

public class GetPortsEndpoint : EndpointWithoutRequest<string[]>
{
    public override void Configure()
    {
        Get("/" + ApiRoutes.ConfigPorts);
        Group<ConfigGroup>();
        Version(1);
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var result = SerialPort.GetPortNames();
        await SendAsync(result, cancellation: ct);
    }
}