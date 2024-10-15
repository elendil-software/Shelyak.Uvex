using Shelyak.Usis;
using Shelyak.Usis.Responses;
using Shelyak.Uvex.Alpaca;
using Shelyak.Uvex.Web.Endpoints.Spectrograph.Shared;

namespace Shelyak.Uvex.Web.Endpoints.Spectrograph.Device;

public class ProtocolVersionEndpoint : SpectrographEndpoint<string>
{
    private readonly IUsisDevice _usisDevice;
    
    public ProtocolVersionEndpoint(IUsisDevice usisDevice, IServerTransactionIdProvider serverTransactionIdProvider, ILogger<ProtocolVersionEndpoint> logger) : base(serverTransactionIdProvider, logger)
    {
        _usisDevice = usisDevice;
    }
    
    public override void Configure()
    {
        Get(DeviceNumberRoutePattern + ApiRoutes.ProtocolVersion);
        base.Configure();
    }

    protected override Func<IResponse> UsisFunc() => _usisDevice.GetProtocolVersion;
}