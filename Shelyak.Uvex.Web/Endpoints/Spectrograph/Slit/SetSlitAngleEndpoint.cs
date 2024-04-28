using Shelyak.Usis;
using Shelyak.Uvex.Alpaca;
using Shelyak.Uvex.Web.Core.Alpaca;
using Shelyak.Uvex.Web.Endpoints.Spectrograph.Shared;

namespace Shelyak.Uvex.Web.Endpoints.Spectrograph.Slit;

public class SetSlitAngleEndpoint : SpectrographWithValueEndpoint<float>
{
    private readonly IUsisDevice _usisDevice;

    public SetSlitAngleEndpoint(IUsisDevice usisDevice, IServerTransactionIdProvider serverTransactionIdProvider, ILogger<SetSlitAngleEndpoint> logger)
        : base(serverTransactionIdProvider, logger)
    {
        _usisDevice = usisDevice;
    }
    
    public override void Configure()
    {
        Put(DeviceNumberRoutePattern + ApiRoutes.SlitAngle);
        base.Configure();
    }
    
    public override async Task HandleAsync(AlpacaRequestWithValue<float> request, CancellationToken ct)
    {
        UsisFunc = () => _usisDevice.SetSlitAngle(request.Value);
        await base.HandleAsync(request, ct);
    }
}