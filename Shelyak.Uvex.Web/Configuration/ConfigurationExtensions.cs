using Shelyak.Usis;
using Shelyak.Uvex.Web.Settings;

namespace Shelyak.Uvex.Web.Configuration;

public static class ConfigurationExtensions
{
    public static WebApplicationBuilder AddConfiguration(this WebApplicationBuilder builder)
    {
        builder.Configuration.AddJsonFile(UvexSettingsFilePathProvider.CreateProductionInstance.UvexSettingsFilePath, optional: true, reloadOnChange: true);

        //Configuration
        builder.Services.Configure<SerialPortSettings>(builder.Configuration.GetSection("SerialPortSettings"));
        builder.Services.Configure<UvexControlsSettings>(builder.Configuration.GetSection(SwaggerSettings.SectionName));
        builder.Services.Configure<UvexControlsSettings>(builder.Configuration.GetSection(UvexControlsSettings.SectionName));

        return builder;
    }
}