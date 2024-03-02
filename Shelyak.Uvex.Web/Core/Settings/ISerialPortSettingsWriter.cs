using Shelyak.Usis;

namespace Shelyak.Uvex.Web.Core.Settings;

public interface ISerialPortSettingsWriter
{
    Task Write(SerialPortSettings serialPortSettings);
}