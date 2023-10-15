using System.ComponentModel.DataAnnotations;
using System.IO.Ports;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using Shelyak.Usis;
using Shelyak.Uvex.WebApi.Settings;

namespace Shelyak.Uvex.WebApi.Pages;

public class Configuration : PageModel
{
    private readonly ISerialPortSettingsWriter _serialPortSettingsWriter;
    private readonly SerialPortSettings _serialPortSettingsOptions;


    public Configuration(ISerialPortSettingsWriter serialPortSettingsWriter, IOptionsSnapshot<SerialPortSettings> serialPortSettingsOptions)
    {
        _serialPortSettingsWriter = serialPortSettingsWriter;
        _serialPortSettingsOptions = serialPortSettingsOptions.Value;
    }

    public SelectList ComPorts { get; set; }
    
    [BindProperty]
    [Display(Name = "UVEX COM Port")]
    public string SelectedComPort { get; set; } = string.Empty;
    
    [TempData]
    public string Message { get; set; }
    [TempData]
    public string MessageType { get; set; }
    
    public void OnGet()
    {
        InitModel();
    }
        
    public IActionResult OnPost()
    {
        
        if (ModelState.IsValid)
        {
            _serialPortSettingsOptions.PortName = SelectedComPort;
            _serialPortSettingsWriter.Write(_serialPortSettingsOptions);
            Message = "Paramètres enregistrés";
            MessageType = "success";
            return RedirectToPage("Configuration");
        }
        
        InitModel();
        Message = "Une erreur est survenue";
        MessageType = "danger";
        return Page();
    }

    private void InitModel()
    {
        SelectedComPort = _serialPortSettingsOptions.PortName;
        ComPorts = GetComPortOptions();
    }

    private SelectList GetComPortOptions()
    {
        var comPorts = SerialPort.GetPortNames().ToList();
        return new SelectList(comPorts);
    }
}