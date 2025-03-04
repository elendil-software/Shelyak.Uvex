﻿using Shelyak.Usis;
using Shelyak.Usis.Responses;
using Shelyak.Uvex.Alpaca;
using Shelyak.Uvex.Web.Endpoints.Spectrograph.Shared;

namespace Shelyak.Uvex.Web.Endpoints.Spectrograph.Device;

public class HumidityEndpoint : SpectrographEndpoint<string>
{
    private readonly IUsisDevice _usisDevice;
    
    public HumidityEndpoint(IUsisDevice usisDevice, IServerTransactionIdProvider serverTransactionIdProvider, ILogger<HumidityEndpoint> logger) : base(serverTransactionIdProvider, logger)
    {
        _usisDevice = usisDevice;
    }
    
    public override void Configure()
    {
        Get(DeviceNumberRoutePattern + ApiRoutes.Humidity);
        base.Configure();
    }

    protected override Func<IResponse> UsisFunc() => _usisDevice.GetHumidity;
}