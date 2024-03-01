using System.IO.Ports;
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

    [HttpGet("Port")]
    public IActionResult GetPort()
    {
        return Ok(SerialPortSettingsOptions.Value.PortName);
    }


    [HttpPut("Port")]
    public async Task<IActionResult> UpdatePort([FromBody] string portName)
    {
        var serialPortSettings = SerialPortSettingsOptions.Value;
        serialPortSettings.PortName = portName;

        await _serialPortSettingsWriter.Write(serialPortSettings);
        return Ok();
    }

    [HttpGet("Ports")]
    public IActionResult GetPorts()
    {
        return Ok(SerialPort.GetPortNames());
    }
}