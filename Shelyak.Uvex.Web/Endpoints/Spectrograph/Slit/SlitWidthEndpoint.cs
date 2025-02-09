﻿using Shelyak.Usis;
using Shelyak.Usis.Responses;
using Shelyak.Uvex.Alpaca;
using Shelyak.Uvex.Web.Endpoints.Spectrograph.Shared;

namespace Shelyak.Uvex.Web.Endpoints.Spectrograph.Slit;

public class SlitWidthEndpoint : SpectrographEndpoint<float>
{
    private readonly IUsisDevice _usisDevice;

    public SlitWidthEndpoint(IUsisDevice usisDevice, IServerTransactionIdProvider serverTransactionIdProvider, ILogger<SlitWidthEndpoint> logger)
        : base(serverTransactionIdProvider, logger)
    {
        _usisDevice = usisDevice;
    }
    
    public override void Configure()
    {
        Get(DeviceNumberRoutePattern + ApiRoutes.SlitWidth);
        base.Configure();
    }

    protected override Func<IResponse> UsisFunc() => _usisDevice.GetSlitWidth;
}