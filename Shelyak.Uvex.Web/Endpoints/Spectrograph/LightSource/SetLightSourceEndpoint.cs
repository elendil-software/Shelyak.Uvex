using Shelyak.Usis;
using Shelyak.Uvex.Alpaca;
using Shelyak.Uvex.Web.Endpoints.Spectrograph.Shared;

namespace Shelyak.Uvex.Web.Endpoints.Spectrograph.LightSource;

public class SetLightSourceEndpoint : SpectrographWithValueEndpoint<Usis.Enums.LightSource>
{
    private readonly IUsisDevice _usisDevice;

    public SetLightSourceEndpoint(IUsisDevice usisDevice, IServerTransactionIdProvider serverTransactionIdProvider, ILogger<SetLightSourceEndpoint> logger)
        : base(serverTransactionIdProvider, logger)
    {
        _usisDevice = usisDevice;
    }
    
    public override void Configure()
    {
        Put(DeviceNumberRoutePattern + ApiRoutes.LightSource);
        base.Configure();
    }

    public override async Task HandleAsync(AlpacaRequestWithValue<Usis.Enums.LightSource> request, CancellationToken ct)
    {
        UsisFunc = () => _usisDevice.SetLightSource(request.Value);
        await base.HandleAsync(request, ct);
    }
}