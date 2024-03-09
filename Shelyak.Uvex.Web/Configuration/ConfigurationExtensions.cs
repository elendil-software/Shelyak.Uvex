using Shelyak.Usis;

namespace Shelyak.Uvex.Web.Configuration;

public static class ConfigurationExtensions
{
    public static WebApplicationBuilder AddConfiguration(this WebApplicationBuilder builder)
    {
        builder.Configuration.AddJsonFile(UvexSettingsFilePathProvider.UvexSettingsFilePath, optional: true, reloadOnChange: true);

        //Configuration
        builder.Services.Configure<SerialPortSettings>(builder.Configuration.GetSection("SerialPortSettings"));

        return builder;
    }
}