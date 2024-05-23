
using Shelyak.Uvex.Web.Locales;

namespace Shelyak.Uvex.Web.Configuration;

public static class LocalizationConfigurationExtension
{
    public static IApplicationBuilder UseLocalization(this IApplicationBuilder app)
    {
        string[] supportedCultures = Cultures.SupportedCultures.Select(c => c.Name).ToArray();
        var localizationOptions = new RequestLocalizationOptions()
            .SetDefaultCulture(supportedCultures[0])
            .AddSupportedCultures(supportedCultures)
            .AddSupportedUICultures(supportedCultures);

        app.UseRequestLocalization(localizationOptions);
        
        return app;
    }
}