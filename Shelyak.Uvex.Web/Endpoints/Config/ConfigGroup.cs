using FastEndpoints;

namespace Shelyak.Uvex.Web.Endpoints.Config;

internal class ConfigGroup : Group
{
    public const string RoutePrefix = "Config";
    
    public ConfigGroup()
    {
        Configure(RoutePrefix, ep => { ep.Description(x => x.WithTags("Config")); });
    }
}