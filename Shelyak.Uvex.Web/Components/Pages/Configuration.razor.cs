using System.ComponentModel.DataAnnotations;
using System.IO.Ports;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;
using Shelyak.Usis;
using Shelyak.Uvex.Web.Settings;

namespace Shelyak.Uvex.Web.Components.Pages;

public partial class Configuration
{
    [Inject] 
    private ISerialPortSettingsWriter SerialPortSettingsWriter { get; set; }
    [Inject] 
    private IOptionsSnapshot<SerialPortSettings> SerialPortSettingsOptions { get; set; }
    [Inject]
    private ILogger<Configuration> Logger { get; set; }
    
    [SupplyParameterFromForm] 
    private EditConfigurationModel Model { get; set; } = new();

    private string Message { get; set; } = string.Empty;
    private string MessageType { get; set; } = string.Empty;

    protected override void OnInitialized()
    {
        Model = new EditConfigurationModel
        {
            SelectedComPort = SerialPortSettingsOptions.Value.PortName,
            ComPorts = SerialPort.GetPortNames().ToList()
        };
        
        Model.ComPorts.Add("COM1");
        Model.ComPorts.Add("COM2");
        
        base.OnInitialized();
    }

    private async Task SubmitForm()
    {
        try
        {
            var serialPortSettings = SerialPortSettingsOptions.Value;
            serialPortSettings.PortName = Model.SelectedComPort;
            await SerialPortSettingsWriter.Write(serialPortSettings);
            Message = "Paramètres enregistrés";
            MessageType = "success";
            Logger.LogInformation("Serial port settings saved");
        }
        catch (Exception e)
        {
            Message = "Une erreur est survenue";
            MessageType = "danger";
            Logger.LogError(e, "Error while saving serial port settings");
        }
    }
    
    private sealed class EditConfigurationModel
    {
        public List<string> ComPorts { get; set; } = new();

        [Required] public string SelectedComPort { get; set; } = string.Empty;
    }
}

