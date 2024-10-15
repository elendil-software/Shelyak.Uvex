using Shelyak.Usis;
using Shelyak.Usis.Responses;
using Shelyak.Uvex.Alpaca;
using Shelyak.Uvex.Web.Endpoints.Spectrograph.Shared;

namespace Shelyak.Uvex.Web.Endpoints.Spectrograph.Device;

public class DeviceNameEndpoint : SpectrographEndpoint<string>
{
    private readonly IUsisDevice _usisDevice;

    public DeviceNameEndpoint(IUsisDevice usisDevice, IServerTransactionIdProvider serverTransactionIdProvider, ILogger<DeviceNameEndpoint> logger) : base(serverTransactionIdProvider, logger)
    {
        _usisDevice = usisDevice;
    }
    
    public override void Configure()
    {
        Get(DeviceNumberRoutePattern + ApiRoutes.DeviceName);
        base.Configure();
    }

    protected override Func<IResponse<string>> UsisFunc() => _usisDevice.GetDeviceName;
}