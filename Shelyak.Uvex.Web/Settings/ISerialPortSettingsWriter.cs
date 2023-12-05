using Shelyak.Usis;

namespace Shelyak.Uvex.Web.Settings;

public interface ISerialPortSettingsWriter
{
    void Write(SerialPortSettings serialPortSettings);
}