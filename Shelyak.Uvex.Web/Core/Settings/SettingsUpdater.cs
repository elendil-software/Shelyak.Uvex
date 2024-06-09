using System.Text;
using System.Text.Json;
using Shelyak.Usis;
using Shelyak.Uvex.Web.Configuration;
using Shelyak.Uvex.Web.Settings;

namespace Shelyak.Uvex.Web.Core.Settings;

public class SettingsUpdater : ISettingsUpdater
{
    private static readonly SemaphoreSlim Semaphore = new(1, 1);
    private readonly string _settingsFilePath;

    public SettingsUpdater(IUvexSettingsFilePathProvider settingsFilePathProvider)
    {
        _settingsFilePath = settingsFilePathProvider.UvexSettingsFilePath;
    }
    
    public Task UpdateSerialPort(SerialPortSettings serialPortSettings)
    {
        return Update(settings => 
        {
            settings.SerialPort = serialPortSettings;
            return settings;
        });
    }    
    
    public Task UpdateSwagger(bool enabled)
    {
        return Update(settings => 
        {
            settings.Swagger = new SwaggerSettings { Enabled = enabled };
            return settings;
        });
    }

    public Task UpdateGratingAngleStepSize(float stepSize)
    {
        return Update(settings =>
        {
            settings.UvexControls.GratingAngleStepSize = stepSize;
            return settings;
        });
    }

    public Task UpdateGratingWavelengthStepSize(float stepSize)
    {
        return Update(settings =>
        {
            settings.UvexControls.GratingWavelengthStepSize = stepSize;
            return settings;
        });
    }

    public Task UpdateFocusStepSize(float stepSize)
    {
        return Update(settings =>
        {
            settings.UvexControls.FocusStepSize = stepSize;
            return settings;
        });
    }

    private async Task Update(Func<UvexSettings, UvexSettings> updateFunc)
    {
        await Semaphore.WaitAsync();
        try
        {
            var settings = ReadJsonFile();
            settings = updateFunc(settings);
            await WriteJsonFile(settings);
        }
        finally
        {
            Semaphore.Release();
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