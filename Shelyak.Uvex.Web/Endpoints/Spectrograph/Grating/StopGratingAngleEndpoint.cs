using Shelyak.Usis;
using Shelyak.Usis.Responses;
using Shelyak.Uvex.Alpaca;
using Shelyak.Uvex.Web.Core.Alpaca;
using Shelyak.Uvex.Web.Endpoints.Spectrograph.Shared;

namespace Shelyak.Uvex.Web.Endpoints.Spectrograph.Grating;

public class StopGratingAngleEndpoint : SpectrographEndpoint<float>
{
    private readonly IUsisDevice _usisDevice;

    public StopGratingAngleEndpoint(IUsisDevice usisDevice, IServerTransactionIdProvider serverTransactionIdProvider, ILogger<StopGratingAngleEndpoint> logger)
        : base(serverTransactionIdProvider, logger)
    {
        _usisDevice = usisDevice;
    }
    
    public override void Configure()
    {
        Put(DeviceNumberRoutePattern + ApiRoutes.StopGratingAngle);
        base.Configure();
    }

    protected override Func<IResponse<float>> UsisFunc() => _usisDevice.StopGratingAngle;
}