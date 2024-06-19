using System.Diagnostics;

namespace Shelyak.Uvex.Web.Configuration;

public static class BrowserStarter
{
    public static void StartBrowser(this WebApplicationBuilder builder, bool isStartedFromAscom)
    {
        if (!isStartedFromAscom)
        {
            Task.Run(async () =>
            {
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