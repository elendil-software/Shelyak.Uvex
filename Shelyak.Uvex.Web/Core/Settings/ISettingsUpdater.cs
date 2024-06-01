using Shelyak.Usis;
using Shelyak.Uvex.Web.Settings;

namespace Shelyak.Uvex.Web.Core.Settings;

public interface ISettingsUpdater
{
    Task UpdateSerialPort(SerialPortSettings serialPortSettings);
    Task UpdateSwagger(bool enabled);
}