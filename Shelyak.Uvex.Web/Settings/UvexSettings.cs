using Shelyak.Usis;

namespace Shelyak.Uvex.Web.Settings;

public class UvexSettings
{
    public SerialPortSettings SerialPortSettings { get; set; } = new();
    public SwaggerSettings Swagger { get; set; } = new();
}