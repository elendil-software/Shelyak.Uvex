using Shelyak.Usis;
using Shelyak.Usis.Responses;
using Shelyak.Uvex.Alpaca;
using Shelyak.Uvex.Web.Core.Alpaca;
using Shelyak.Uvex.Web.Endpoints.Spectrograph.Shared;

namespace Shelyak.Uvex.Web.Endpoints.Spectrograph.Device;

public class SoftwareVersionEndpoint : SpectrographEndpoint<string>
{
    private readonly IUsisDevice _usisDevice;
    
    public SoftwareVersionEndpoint(IUsisDevice usisDevice, IServerTransactionIdProvider serverTransactionIdProvider, ILogger<SoftwareVersionEndpoint> logger) : base(serverTransactionIdProvider, logger)
    {
        _usisDevice = usisDevice;
    }
    
    public override void Configure()
    {
        Get(DeviceNumberRoutePattern + ApiRoutes.SoftwareVersion);
        base.Configure();
    }

    protected override Func<IResponse<string>> UsisFunc() => _usisDevice.GetSoftwareVersion;
}