using Shelyak.Usis;
using Shelyak.Usis.Responses;
using Shelyak.Uvex.Alpaca;
using Shelyak.Uvex.Web.Endpoints.Spectrograph.Shared;

namespace Shelyak.Uvex.Web.Endpoints.Spectrograph.Grating;

public class GratingWaveLengthPrecEndpoint : SpectrographEndpoint<float>
{
    private readonly IUsisDevice _usisDevice;

    public GratingWaveLengthPrecEndpoint(IUsisDevice usisDevice, IServerTransactionIdProvider serverTransactionIdProvider, ILogger<GratingWaveLengthPrecEndpoint> logger)
        : base(serverTransactionIdProvider, logger)
    {
        _usisDevice = usisDevice;
    }
    
    public override void Configure()
    {
        Get(DeviceNumberRoutePattern + ApiRoutes.GratingWaveLengthPrec);
        base.Configure();
    }

    protected override Func<IResponse> UsisFunc() => _usisDevice.GetGratingWaveLengthPrec;
}