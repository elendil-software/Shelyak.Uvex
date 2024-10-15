using Shelyak.Usis;
using Shelyak.Usis.Responses;
using Shelyak.Uvex.Alpaca;
using Shelyak.Uvex.Web.Endpoints.Spectrograph.Shared;

namespace Shelyak.Uvex.Web.Endpoints.Spectrograph.LightSource;

public class LightSourceEndpoint : SpectrographEndpoint<Usis.Enums.LightSource>
{
    private readonly IUsisDevice _usisDevice;

    public LightSourceEndpoint(IUsisDevice usisDevice, IServerTransactionIdProvider serverTransactionIdProvider, ILogger<LightSourceEndpoint> logger)
        : base(serverTransactionIdProvider, logger)
    {
        _usisDevice = usisDevice;
    }
    
    public override void Configure()
    {
        Get(DeviceNumberRoutePattern + ApiRoutes.LightSource);
        base.Configure();
    }
    
    protected override Func<IResponse> UsisFunc() => _usisDevice.GetLightSource;
}