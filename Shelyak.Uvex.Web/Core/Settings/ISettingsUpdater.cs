using Shelyak.Usis;

namespace Shelyak.Uvex.Web.Core.Settings;

public interface ISettingsUpdater
{
    Task UpdateSerialPort(SerialPortSettings serialPortSettings);
}