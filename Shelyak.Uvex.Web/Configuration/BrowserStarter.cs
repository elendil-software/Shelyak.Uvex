using System.Diagnostics;
using Serilog;

namespace Shelyak.Uvex.Web.Configuration;

public static class BrowserStarter
{
    public static void StartBrowser(this WebApplicationBuilder builder, bool isStartedFromAscom)
    {
        if (builder.Environment.IsDevelopment())
        {
            Log.Information("Development mode skip start browser");
            return;
        }

        if (isStartedFromAscom)
        {
            Log.Information("Started from ASCOM skip start browser");
        }
        else 
        {
            Log.Information("Starting browser");
            Task.Run(async () =>
            {
                //Delay to allow the server to start and avoid the browser to open too early
                await Task.Delay(TimeSpan.FromSeconds(3));

                Process.Start(new ProcessStartInfo
                {
                    FileName = builder.Configuration.GetApplicationUrl(),
                    UseShellExecute = true
                });
            });
        }
    }
}