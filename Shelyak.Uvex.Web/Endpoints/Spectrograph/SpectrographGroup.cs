using FastEndpoints;

namespace Shelyak.Uvex.Web.Endpoints.Spectrograph;

internal class SpectrographGroup : Group
{
    public const string RoutePrefix = "spectrograph";
    
    public SpectrographGroup()
    {
        Configure(RoutePrefix, ep => { ep.Description(x => x.WithTags("Spectrograph")); });
    }
}