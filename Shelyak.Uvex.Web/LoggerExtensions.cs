using Serilog;
using Serilog.Configuration;

namespace Shelyak.Uvex.WebApi;

public static class LoggerExtensions
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
}