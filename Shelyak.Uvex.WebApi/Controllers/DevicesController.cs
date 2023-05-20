using Microsoft.AspNetCore.Mvc;

namespace Shelyak.Uvex.WebApi.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class DeviceController : ControllerBase
{
    
    [HttpGet]
    [Route("{deviceNumber}/Action1")]
    public IActionResult Action1(int deviceNumber)
    {
        return Ok($"Command 'Action1' executed for device n°{deviceNumber}");
    }
    
    [HttpGet]
    [Route("{deviceNumber}/Test")]
    public IActionResult Action2(int deviceNumber)
    {
        return Ok($"Command 'Action2' executed for device n°{deviceNumber}");
    }
}