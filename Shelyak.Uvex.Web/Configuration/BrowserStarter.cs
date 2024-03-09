using System.Diagnostics;

namespace Shelyak.Uvex.Web.Configuration;

public static class BrowserStarter
{
    public static void StartBrowser(this WebApplicationBuilder builder, bool isStartedFromAscom)
    {
        if (!isStartedFromAscom)
        {
            Process.Start(new ProcessStartInfo()
            {
                FileName = builder.Configuration.GetSection("Urls:Web").Value,
                UseShellExecute = true
            });
        }
    }
}