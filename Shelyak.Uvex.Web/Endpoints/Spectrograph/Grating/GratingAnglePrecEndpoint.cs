﻿using Shelyak.Usis;
using Shelyak.Usis.Responses;
using Shelyak.Uvex.Alpaca;
using Shelyak.Uvex.Web.Endpoints.Spectrograph.Shared;

namespace Shelyak.Uvex.Web.Endpoints.Spectrograph.Grating;

public class GratingAnglePrecEndpoint : SpectrographEndpoint<float>
{
    private readonly IUsisDevice _usisDevice;

    public GratingAnglePrecEndpoint(IUsisDevice usisDevice, IServerTransactionIdProvider serverTransactionIdProvider, ILogger<GratingAnglePrecEndpoint> logger)
        : base(serverTransactionIdProvider, logger)
    {
        _usisDevice = usisDevice;
    }
    
    public override void Configure()
    {
        Get(DeviceNumberRoutePattern + ApiRoutes.GratingAnglePrec);
        base.Configure();
    }

    protected override Func<IResponse<float>> UsisFunc() => _usisDevice.GetGratingAnglePrec;
}