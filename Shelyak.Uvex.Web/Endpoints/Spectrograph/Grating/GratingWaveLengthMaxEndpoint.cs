using Shelyak.Usis;
using Shelyak.Usis.Responses;
using Shelyak.Uvex.Alpaca;
using Shelyak.Uvex.Web.Endpoints.Spectrograph.Shared;

namespace Shelyak.Uvex.Web.Endpoints.Spectrograph.Grating;

public class GratingWaveLengthMaxEndpoint : SpectrographEndpoint<float>
{
    private readonly IUsisDevice _usisDevice;

    public GratingWaveLengthMaxEndpoint(IUsisDevice usisDevice, IServerTransactionIdProvider serverTransactionIdProvider, ILogger<GratingWaveLengthMaxEndpoint> logger)
        : base(serverTransactionIdProvider, logger)
    {
        _usisDevice = usisDevice;
    }
    
    public override void Configure()
    {
        Get(DeviceNumberRoutePattern + ApiRoutes.GratingWaveLengthMax);
        base.Configure();
    }

    protected override Func<IResponse> UsisFunc() => _usisDevice.GetGratingWaveLengthMax;
}