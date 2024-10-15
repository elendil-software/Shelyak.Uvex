using Shelyak.Usis;
using Shelyak.Usis.Responses;
using Shelyak.Uvex.Alpaca;
using Shelyak.Uvex.Web.Endpoints.Spectrograph.Shared;

namespace Shelyak.Uvex.Web.Endpoints.Spectrograph.Focus;

public class StopFocusPositionEndpoint : SpectrographEndpoint<float>
{
    private readonly IUsisDevice _usisDevice;

    public StopFocusPositionEndpoint(IUsisDevice usisDevice, IServerTransactionIdProvider serverTransactionIdProvider, ILogger<StopFocusPositionEndpoint> logger)
        : base(serverTransactionIdProvider, logger)
    {
        _usisDevice = usisDevice;
    }
    
    public override void Configure()
    {
        Put(DeviceNumberRoutePattern + ApiRoutes.StopFocusPosition);
        base.Configure();
    }
    
    protected override Func<IResponse> UsisFunc() => _usisDevice.StopFocusPosition;
}