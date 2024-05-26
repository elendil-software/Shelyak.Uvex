using Microsoft.AspNetCore.Mvc;
using Shelyak.Usis;
using Shelyak.Usis.Responses;
using Shelyak.Uvex.Web.Core.Alpaca;
using Shelyak.Uvex.Web.Core.HttpClients;
using Shelyak.Uvex.Web.Core.Settings;

namespace Shelyak.Uvex.Web.Configuration;

public static class ServicesExtension
{
    public static WebApplicationBuilder AddServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<IUsisDevice, UsisDevice>();
        builder.Services.AddSingleton<ICommandSender, SerialPortCommandSender>();
        builder.Services.AddSingleton<IResponseParser, ResponseParser>();
        builder.Services.AddSingleton<IServerTransactionIdProvider, ServerTransactionIdProvider>();
        builder.Services.AddSingleton<ISettingsUpdater>(new SettingsUpdater(UvexSettingsFilePathProvider.UvexSettingsFilePath));

        builder.Services.AddHttpClient<IUvexHttpClient, UvexHttpClient>().ConfigureHttpClient((provider, client) =>
        {
            var config = provider.GetRequiredService<IConfiguration>();
            var baseAddress = config.GetSection("Urls:Api").Value;
            client.BaseAddress = new Uri(baseAddress);
        });

        builder.Services.AddOutputCache(options =>
        {
            options.DefaultExpirationTimeSpan = TimeSpan.FromSeconds(1);
            options.AddBasePolicy(build => build.Cache());
        });
        
        return builder;
    }
}