using Shelyak.Usis;
using Shelyak.Uvex.Alpaca;
using Shelyak.Uvex.Web.Core.Alpaca;
using Shelyak.Uvex.Web.Endpoints.Spectrograph.Shared;

namespace Shelyak.Uvex.Web.Endpoints.Spectrograph.Grating;

public class SetGratingDensityEndpoint : SpectrographWithValueEndpoint<float>
{
    private readonly IUsisDevice _usisDevice;

    public SetGratingDensityEndpoint(IUsisDevice usisDevice, IServerTransactionIdProvider serverTransactionIdProvider, ILogger<SetGratingDensityEndpoint> logger)
        : base(serverTransactionIdProvider, logger)
    {
        _usisDevice = usisDevice;
    }
    
    public override void Configure()
    {
        Put(DeviceNumberRoutePattern + ApiRoutes.GratingDensity);
        base.Configure();
    }
    
    public override async Task HandleAsync(AlpacaRequestWithValue<float> request, CancellationToken ct)
    {
        UsisFunc = () => _usisDevice.SetGratingDensity(request.Value);
        await base.HandleAsync(request, ct);
    }
}