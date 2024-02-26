using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Shelyak.Usis;
using Shelyak.Uvex.Web.Settings;

namespace Shelyak.Uvex.Web.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class ConfigController : ControllerBase
{
    private readonly ISerialPortSettingsWriter _serialPortSettingsWriter;
    private IOptionsSnapshot<SerialPortSettings> SerialPortSettingsOptions { get; set; }

    public ConfigController(ISerialPortSettingsWriter serialPortSettingsWriter, IOptionsSnapshot<SerialPortSettings> serialPortSettingsOptions)
    {
        _serialPortSettingsWriter = serialPortSettingsWriter;
        SerialPortSettingsOptions = serialPortSettingsOptions;
    }
    
    [HttpGet("GetPort")]
    public IActionResult GetPort()
    {
        return Ok(SerialPortSettingsOptions.Value.PortName);
    }


    [HttpPut("UpdatePort")]
    public async Task<IActionResult> UpdatePort(string portName)
    {
        var serialPortSettings = SerialPortSettingsOptions.Value;
        serialPortSettings.PortName = portName;
        
        await _serialPortSettingsWriter.Write(serialPortSettings);
        return Ok();
    }
}