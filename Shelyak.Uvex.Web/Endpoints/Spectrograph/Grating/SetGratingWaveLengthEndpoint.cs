using Shelyak.Usis;
using Shelyak.Uvex.Alpaca;
using Shelyak.Uvex.Web.Core.Alpaca;
using Shelyak.Uvex.Web.Endpoints.Spectrograph.Shared;

namespace Shelyak.Uvex.Web.Endpoints.Spectrograph.Grating;

public class SetGratingWaveLengthEndpoint : SpectrographWithValueEndpoint<float>
{
    private readonly IUsisDevice _usisDevice;

    public SetGratingWaveLengthEndpoint(IUsisDevice usisDevice, IServerTransactionIdProvider serverTransactionIdProvider, ILogger<SetGratingWaveLengthEndpoint> logger)
        : base(serverTransactionIdProvider, logger)
    {
        _usisDevice = usisDevice;
    }
    
    public override void Configure()
    {
        Put(DeviceNumberRoutePattern + ApiRoutes.GratingWaveLength);
        base.Configure();
    }
    
    public override async Task HandleAsync(AlpacaRequestWithValue<float> request, CancellationToken ct)
    {
        UsisFunc = () => _usisDevice.SetGratingWaveLength(request.Value);
        await base.HandleAsync(request, ct);
    }
}