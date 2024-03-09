using Serilog;
using Serilog.Configuration;

namespace Shelyak.Uvex.Web.Configuration;

public static class LoggerConfigurationExtensions
{
    public static LoggerConfiguration AppSettingsConfiguration(this LoggerSettingsConfiguration settingConfiguration)
    {
        string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";

        IConfigurationBuilder config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables();

        return settingConfiguration.Configuration(config.Build());
    }
    
    public static WebApplicationBuilder UseSerilog(this WebApplicationBuilder builder)
    {
        builder.Host.UseSerilog();
        return builder;
    }
}