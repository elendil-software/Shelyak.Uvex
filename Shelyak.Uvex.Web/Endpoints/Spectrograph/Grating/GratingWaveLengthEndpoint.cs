using Shelyak.Usis;
using Shelyak.Usis.Responses;
using Shelyak.Uvex.Alpaca;
using Shelyak.Uvex.Web.Core.Alpaca;
using Shelyak.Uvex.Web.Endpoints.Spectrograph.Shared;

namespace Shelyak.Uvex.Web.Endpoints.Spectrograph.Grating;

public class GratingWaveLengthEndpoint : SpectrographEndpoint<float>
{
    private readonly IUsisDevice _usisDevice;

    public GratingWaveLengthEndpoint(IUsisDevice usisDevice, IServerTransactionIdProvider serverTransactionIdProvider, ILogger<GratingWaveLengthEndpoint> logger)
        : base(serverTransactionIdProvider, logger)
    {
        _usisDevice = usisDevice;
    }
    
    public override void Configure()
    {
        Get(DeviceNumberRoutePattern + ApiRoutes.GratingWaveLength);
        base.Configure();
    }

    protected override Func<IResponse<float>> UsisFunc() => _usisDevice.GetGratingWaveLength;
}