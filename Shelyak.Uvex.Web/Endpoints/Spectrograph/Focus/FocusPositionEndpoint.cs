using Shelyak.Usis;
using Shelyak.Usis.Responses;
using Shelyak.Uvex.Alpaca;
using Shelyak.Uvex.Web.Core.Alpaca;
using Shelyak.Uvex.Web.Endpoints.Spectrograph.Shared;

namespace Shelyak.Uvex.Web.Endpoints.Spectrograph.Focus;

public class FocusPositionEndpoint : SpectrographEndpoint<float>
{
    private readonly IUsisDevice _usisDevice;

    public FocusPositionEndpoint(IUsisDevice usisDevice, IServerTransactionIdProvider serverTransactionIdProvider, ILogger<FocusPositionEndpoint> logger)
        : base(serverTransactionIdProvider, logger)
    {
        _usisDevice = usisDevice;
    }
    
    public override void Configure()
    {
        Get(DeviceNumberRoutePattern + ApiRoutes.FocusPosition);
        base.Configure();
    }

    protected override Func<IResponse<float>> UsisFunc() => _usisDevice.GetFocusPosition;
}