using Shelyak.Usis;
using Shelyak.Usis.Responses;
using Shelyak.Uvex.Alpaca;
using Shelyak.Uvex.Web.Endpoints.Spectrograph.Shared;

namespace Shelyak.Uvex.Web.Endpoints.Spectrograph.Grating;

public class GratingWaveLengthMinEndpoint : SpectrographEndpoint<float>
{
    private readonly IUsisDevice _usisDevice;

    public GratingWaveLengthMinEndpoint(IUsisDevice usisDevice, IServerTransactionIdProvider serverTransactionIdProvider, ILogger<GratingWaveLengthMinEndpoint> logger)
        : base(serverTransactionIdProvider, logger)
    {
        _usisDevice = usisDevice;
    }
    
    public override void Configure()
    {
        Get(DeviceNumberRoutePattern + ApiRoutes.GratingWaveLengthMin);
        base.Configure();
    }

    protected override Func<IResponse> UsisFunc() => _usisDevice.GetGratingWaveLengthMin;
}