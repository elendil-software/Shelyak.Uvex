using System.ComponentModel.DataAnnotations;
using System.IO.Ports;
using BlazorBootstrap;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;
using Shelyak.Usis;
using Shelyak.Uvex.Web.Settings;

namespace Shelyak.Uvex.Web.Components.Pages;

public partial class Configuration
{
    [Inject] protected ToastService ToastService { get; set; }
    [Inject] private ISerialPortSettingsWriter SerialPortSettingsWriter { get; set; }
    [Inject] private IOptionsSnapshot<SerialPortSettings> SerialPortSettingsOptions { get; set; }
    [Inject] private ILogger<Configuration> Logger { get; set; }

    private EditConfigurationModel Model { get; set; } = new();
    
    
    protected override void OnInitialized()
    {
        Model = new EditConfigurationModel
        {
            SelectedComPort = SerialPortSettingsOptions.Value.PortName,
            ComPorts = SerialPort.GetPortNames().ToList()
        };
    }

    private async Task SubmitForm()
    {
        try
        {
            var serialPortSettings = SerialPortSettingsOptions.Value;
            serialPortSettings.PortName = Model.SelectedComPort;
            await SerialPortSettingsWriter.Write(serialPortSettings);
            ToastService.Notify(new(ToastType.Success, "Paramètres enregistrés"));
            Logger.LogInformation("Serial port settings saved");
        }
        catch (Exception e)
        {
            ToastService.Notify(new(ToastType.Danger, "Une erreur est survenue"));
            Logger.LogError(e, "Error while saving serial port settings");
        }
    }
    
    private sealed class EditConfigurationModel
    {
        public List<string> ComPorts { get; set; } = new();

        [Required] public string SelectedComPort { get; set; } = string.Empty;
    }
}

