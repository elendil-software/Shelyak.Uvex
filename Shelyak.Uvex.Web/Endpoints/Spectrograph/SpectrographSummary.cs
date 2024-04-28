using FastEndpoints;

namespace Shelyak.Uvex.Web.Endpoints.Spectrograph;

public abstract class SpectrographSummary<TEndpoint> : Summary<TEndpoint> where TEndpoint : IEndpoint
{
    protected SpectrographSummary()
    {
        Response(200);
        Response(400);
        Response(500);
    }
}