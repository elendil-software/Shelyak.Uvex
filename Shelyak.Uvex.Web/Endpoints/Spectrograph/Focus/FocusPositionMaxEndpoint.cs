using Shelyak.Usis;
using Shelyak.Usis.Responses;
using Shelyak.Uvex.Alpaca;
using Shelyak.Uvex.Web.Endpoints.Spectrograph.Shared;

namespace Shelyak.Uvex.Web.Endpoints.Spectrograph.Focus;

public class FocusPositionMaxEndpoint : SpectrographEndpoint<float>
{
    private readonly IUsisDevice _usisDevice;

    public FocusPositionMaxEndpoint(IUsisDevice usisDevice, IServerTransactionIdProvider serverTransactionIdProvider, ILogger<FocusPositionMaxEndpoint> logger)
        : base(serverTransactionIdProvider, logger)
    {
        _usisDevice = usisDevice;
    }

    public override void Configure()
    {
        Get(DeviceNumberRoutePattern + ApiRoutes.FocusPositionMax);
        base.Configure();
    }
    
    protected override Func<IResponse<float>> UsisFunc() => _usisDevice.GetFocusPositionMax;
}