using System.Text;
using System.Text.Json;
using Shelyak.Usis;

namespace Shelyak.Uvex.WebApi.Settings;

public class SerialPortSettingsWriter : ISerialPortSettingsWriter
{
    private readonly string _settingsFilePath;

    public SerialPortSettingsWriter(string settingsFilePath)
    {
        _settingsFilePath = settingsFilePath;
    }

    public void Write(SerialPortSettings serialPortSettings)
    {
        string json = "{\"SerialPortSettings\":" + JsonSerializer.Serialize(serialPortSettings) + "}";
        File.WriteAllText(_settingsFilePath, json, Encoding.UTF8);
    }
}