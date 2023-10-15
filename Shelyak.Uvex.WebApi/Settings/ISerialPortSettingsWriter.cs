using Shelyak.Usis;

namespace Shelyak.Uvex.WebApi.Settings;

public interface ISerialPortSettingsWriter
{
    void Write(SerialPortSettings serialPortSettings);
}