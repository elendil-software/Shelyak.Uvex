using System.Text;
using System.Text.Json;
using Shelyak.Usis;

namespace Shelyak.Uvex.Web.Settings;

public class SerialPortSettingsWriter : ISerialPortSettingsWriter
{
    private readonly string _settingsFilePath;

    public SerialPortSettingsWriter(string settingsFilePath)
    {
        _settingsFilePath = settingsFilePath;
    }

    public async Task Write(SerialPortSettings serialPortSettings)
    {
        CreateFolderIfNotExists();
        string json = "{\"SerialPortSettings\":" + JsonSerializer.Serialize(serialPortSettings) + "}";
        await File.WriteAllTextAsync(_settingsFilePath, json, Encoding.UTF8);
    }

    private void CreateFolderIfNotExists()
    {
        var path = Path.GetDirectoryName(_settingsFilePath);
        
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
    }
}