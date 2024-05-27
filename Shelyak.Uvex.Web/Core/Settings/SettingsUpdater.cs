using System.Text;
using System.Text.Json;
using Shelyak.Usis;
using Shelyak.Uvex.Web.Settings;

namespace Shelyak.Uvex.Web.Core.Settings;

public class SettingsUpdater : ISettingsUpdater
{
    private static readonly SemaphoreSlim _semaphore = new(1);
    private readonly string _settingsFilePath;

    public SettingsUpdater(string settingsFilePath)
    {
        _settingsFilePath = settingsFilePath;
    }
    
    public Task UpdateSerialPort(SerialPortSettings serialPortSettings)
    {
        return Update(settings => 
        {
            settings.SerialPortSettings = serialPortSettings;
            return settings;
        });
    }

    private async Task Update(Func<UvexSettings, UvexSettings> updateFunc)
    {
        await _semaphore.WaitAsync();
        try
        {
            var settings = ReadJsonFile();
            settings = updateFunc(settings);
            await WriteJsonFile(settings);
        }
        finally
        {
            _semaphore.Release();
        }
    }
    
    private Task WriteJsonFile(UvexSettings settings)
    {
        CreateFolderIfNotExists();
        var json = JsonSerializer.Serialize(settings, new JsonSerializerOptions { WriteIndented = true });
        return File.WriteAllTextAsync(_settingsFilePath, json, Encoding.UTF8);
    }
    
    private UvexSettings ReadJsonFile()
    {
        if (!File.Exists(_settingsFilePath))
        {
            return new UvexSettings();
        }

        var json = File.ReadAllText(_settingsFilePath);
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