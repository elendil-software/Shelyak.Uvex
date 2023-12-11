using Shelyak.Usis;

namespace Shelyak.Uvex.Web.Settings;

public interface ISerialPortSettingsWriter
{
    Task Write(SerialPortSettings serialPortSettings);
}