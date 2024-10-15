using FastEndpoints;

namespace Shelyak.Uvex.Web.Endpoints.Spectrograph;

public abstract class SpectrographSummary<TEndpoint> : Summary<TEndpoint> where TEndpoint : IEndpoint
{
    protected SpectrographSummary()
    {
        Response();
        Response(400);
        Response(500);
    }
}