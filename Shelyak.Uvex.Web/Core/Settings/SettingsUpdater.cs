using System.Text;
using System.Text.Json;
using Shelyak.Usis;
using Shelyak.Uvex.Web.Settings;

namespace Shelyak.Uvex.Web.Core.Settings;

public class SettingsUpdater : ISettingsUpdater
{
    private readonly object _lock = new();
    private readonly string _settingsFilePath;

    public SettingsUpdater(string settingsFilePath)
    {
        _settingsFilePath = settingsFilePath;
    }
    
    public async Task UpdateSerialPort(SerialPortSettings serialPortSettings)
    {
        await Update(settings => 
        {
            settings.SerialPortSettings = serialPortSettings;
            return settings;
        });
    }
    
    private Task Update(Func<UvexSettings, UvexSettings> updateFunc)
    {
        lock (_lock)
        {
            var settings = ReadJsonFile().Result;
            settings = updateFunc(settings);
            return WriteJsonFile(settings);
        }
    }
    
    private async Task WriteJsonFile(UvexSettings settings)
    {
        CreateFolderIfNotExists();
        var json = JsonSerializer.Serialize(settings);
        await File.WriteAllTextAsync(_settingsFilePath, json, Encoding.UTF8);
    }
    
    private async Task<UvexSettings> ReadJsonFile()
    {
        if (!File.Exists(_settingsFilePath))
        {
            return new UvexSettings();
        }

        var json = await File.ReadAllTextAsync(_settingsFilePath);
        return JsonSerializer.Deserialize<UvexSettings>(json)!;
    }

    private void CreateFolderIfNotExists()
    {
        var path = Path.GetDirectoryName(_settingsFilePath)!;
        
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
    }
}