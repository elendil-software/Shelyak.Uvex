using Shelyak.Usis;
using Shelyak.Usis.Responses;
using Shelyak.Uvex.Alpaca;
using Shelyak.Uvex.Web.Core.Alpaca;
using Shelyak.Uvex.Web.Endpoints.Spectrograph.Shared;

namespace Shelyak.Uvex.Web.Endpoints.Spectrograph.Grating;

public class StopGratingWaveLengthEndpoint : SpectrographEndpoint<float>
{
    private readonly IUsisDevice _usisDevice;

    public StopGratingWaveLengthEndpoint(IUsisDevice usisDevice, IServerTransactionIdProvider serverTransactionIdProvider, ILogger<StopGratingWaveLengthEndpoint> logger)
        : base(serverTransactionIdProvider, logger)
    {
        _usisDevice = usisDevice;
    }
    
    public override void Configure()
    {
        Put(DeviceNumberRoutePattern + ApiRoutes.StopGratingWaveLength);
        base.Configure();
    }
    
    protected override Func<IResponse<float>> UsisFunc() => _usisDevice.StopGratingWaveLength;
}