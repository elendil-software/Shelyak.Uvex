using Shelyak.Usis;
using Shelyak.Uvex.Shared;
using Shelyak.Uvex.Web.Settings;

namespace Shelyak.Uvex.Web.Configuration;

public static class ConfigurationExtensions
{
    
    private const string KestrelUrlKey = "Kestrel:EndPoints:Http:Url";
    
    public static WebApplicationBuilder AddConfiguration(this WebApplicationBuilder builder)
    {
        builder.Configuration.AddJsonFile(UvexSettingsFilePathProvider.CreateProductionInstance.UvexSettingsFilePath, optional: true, reloadOnChange: true);

        //Configuration
        builder.Services.Configure<SerialPortSettings>(builder.Configuration.GetSection(SerialPortSettings.SectionName));
        builder.Services.Configure<SwaggerSettings>(builder.Configuration.GetSection(SwaggerSettings.SectionName));
        builder.Services.Configure<UvexControlsSettings>(builder.Configuration.GetSection(UvexControlsSettings.SectionName));

        return builder;
    }
    
    public static string GetApplicationUrl(this IConfiguration configuration)
    {
        var kestrelUrl = configuration.GetValue<string>(KestrelUrlKey) ?? UvexConst.DefaultUrl;
        return ApplicationUrlHelper.GetApplicationUrl(kestrelUrl);
    }
}