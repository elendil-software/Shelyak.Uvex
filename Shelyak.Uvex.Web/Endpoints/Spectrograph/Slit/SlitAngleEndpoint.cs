using Shelyak.Usis;
using Shelyak.Usis.Responses;
using Shelyak.Uvex.Alpaca;
using Shelyak.Uvex.Web.Core.Alpaca;
using Shelyak.Uvex.Web.Endpoints.Spectrograph.Shared;

namespace Shelyak.Uvex.Web.Endpoints.Spectrograph.Slit;

public class SlitAngleEndpoint : SpectrographEndpoint<float>
{
    private readonly IUsisDevice _usisDevice;

    public SlitAngleEndpoint(IUsisDevice usisDevice, IServerTransactionIdProvider serverTransactionIdProvider, ILogger<SlitAngleEndpoint> logger)
        : base(serverTransactionIdProvider, logger)
    {
        _usisDevice = usisDevice;
    }
    
    public override void Configure()
    {
        Get(DeviceNumberRoutePattern + ApiRoutes.SlitAngle);
        base.Configure();
    }

    protected override Func<IResponse<float>> UsisFunc() => _usisDevice.GetSlitAngle;
}