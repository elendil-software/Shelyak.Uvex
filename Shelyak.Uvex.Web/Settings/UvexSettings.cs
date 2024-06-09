using Shelyak.Usis;

namespace Shelyak.Uvex.Web.Settings;

public class UvexSettings
{
    public SerialPortSettings SerialPort { get; set; } = new();
    public SwaggerSettings Swagger { get; set; } = new();
    public UvexControlsSettings UvexControls { get; set; } = new();
}