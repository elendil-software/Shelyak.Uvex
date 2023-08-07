using Microsoft.AspNetCore.Mvc;
using Shelyak.Usis;
using Shelyak.Usis.Responses;
using Shelyak.Uvex.Alpaca;

namespace Shelyak.Uvex.WebApi.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class SpectrographController : ControllerBase
{
    
    private readonly IUsisDevice _usisDevice;
    private readonly IServerTransactionIdProvider _serverTransactionIdProvider;
    private readonly ILogger<SpectrographController> _logger;

    public SpectrographController(IUsisDevice usisDevice, IServerTransactionIdProvider serverTransactionIdProvider, ILogger<SpectrographController> logger)
    {
        _serverTransactionIdProvider = serverTransactionIdProvider;
        _logger = logger;
        _usisDevice = usisDevice;
    }
    
    #region Device
    
    [HttpGet]
    [Route("{deviceNumber}/devicename")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AlpacaResponse<string>))]
    public IActionResult GetDeviceName(int deviceNumber, uint clientId, uint clientTransactionId)
    {
        return Ok(Execute(() => _usisDevice.GetDeviceName(), deviceNumber, clientId, clientTransactionId));
    }
    
    [HttpGet]
    [Route("{deviceNumber}/softwareversion")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AlpacaResponse<string>))]
    public IActionResult GetSoftwareVersion(int deviceNumber, uint clientId, uint clientTransactionId)
    {
        return Ok(Execute(() => _usisDevice.GetSoftwareVersion(), deviceNumber, clientId, clientTransactionId));
    }
    
    [HttpGet]
    [Route("{deviceNumber}/protocolversion")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AlpacaResponse<string>))]
    public IActionResult GetProtocolVersion(int deviceNumber, uint clientId, uint clientTransactionId)
    {
        return Ok(Execute(() => _usisDevice.GetProtocolVersion(), deviceNumber, clientId, clientTransactionId));
    }
    
    [HttpGet]
    [Route("{deviceNumber}/temperature")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AlpacaResponse<float>))]
    public IActionResult GetTemperature(int deviceNumber, uint clientId, uint clientTransactionId)
    {
        return Ok(Execute(() => _usisDevice.GetTemperature(), deviceNumber, clientId, clientTransactionId));
    }
    
    [HttpGet]
    [Route("{deviceNumber}/humidity")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AlpacaResponse<float>))]
    public IActionResult GetHumidity(int deviceNumber, uint clientId, uint clientTransactionId)
    {
        return Ok(Execute(() => _usisDevice.GetHumidity(), deviceNumber, clientId, clientTransactionId));
    }
    
    #endregion
    
    #region Grating
    
    /// <summary>
    /// Get the current grating ID, if the system can change the grating.
    /// </summary>
    /// <param name="deviceNumber">Zero based device number as set on the server (0 to 4294967295)</param>
    /// <param name="clientId">Client's unique ID. (1 to 4294967295). The client should choose a value at start-up, e.g. a random value between 1 and 65535, and send this on every transaction to associate entries in device logs with this particular client. Zero is a reserved value that clients should not use.</param>
    /// <param name="clientTransactionId">Client's transaction ID. (1 to 4294967295). The client should start this count at 1 and increment by one on each successive transaction. This will aid associating entries in device logs with corresponding entries in client side logs. Zero is a reserved value that clients should not use.</param>
    /// <returns></returns>
    [HttpGet]
    [Route("{deviceNumber}/gratingid")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AlpacaResponse<string>))]
    public IActionResult GetGratingId(int deviceNumber, uint clientId, uint clientTransactionId)
    {
        return Ok(Execute(() => _usisDevice.GetGratingId(), deviceNumber, clientId, clientTransactionId));
    }
    
    [HttpPut]
    [Route("{deviceNumber}/gratingid")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AlpacaResponse<string>))]
    public IActionResult SetGratingId(int deviceNumber, uint clientId, uint clientTransactionId, [FromBody] string value)
    {
        return Ok(Execute(() => _usisDevice.SetGratingId(value), deviceNumber, clientId, clientTransactionId));
    }
    
    [HttpPut]
    [Route("{deviceNumber}/stopgratingid")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AlpacaResponse<string>))]
    public IActionResult StopGratingId(int deviceNumber, uint clientId, uint clientTransactionId)
    {
        return Ok(Execute(() => _usisDevice.StopGratingId(), deviceNumber, clientId, clientTransactionId));
    }
    
    [HttpPut]
    [Route("{deviceNumber}/calibrategratingid")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AlpacaResponse<string>))]
    public IActionResult CalibrateGratingId(int deviceNumber, uint clientId, uint clientTransactionId, string value)
    {
        return Ok(Execute(() => _usisDevice.CalibrateGratingId(value), deviceNumber, clientId, clientTransactionId));
    }
    
    
    [HttpGet]
    [Route("{deviceNumber}/gratingangle")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AlpacaResponse<float>))]
    public IActionResult GetGratingAngle(int deviceNumber, uint clientId, uint clientTransactionId)
    {
        return Ok(Execute(() => _usisDevice.GetGratingAngle(), deviceNumber, clientId, clientTransactionId));
    }
    
    [HttpPut]
    [Route("{deviceNumber}/gratingangle")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AlpacaResponse<float>))]
    public IActionResult SetGratingAngle(int deviceNumber, uint clientId, uint clientTransactionId, [FromBody] float value)
    {
        return Ok(Execute(() => _usisDevice.SetGratingAngle(value), deviceNumber, clientId, clientTransactionId));
    }
    
    [HttpPut]
    [Route("{deviceNumber}/stopgratingangle")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AlpacaResponse<float>))]
    public IActionResult StopGratingAngle(int deviceNumber, uint clientId, uint clientTransactionId)
    {
        return Ok(Execute(() => _usisDevice.StopGratingAngle(), deviceNumber, clientId, clientTransactionId));
    }
    
    [HttpPut]
    [Route("{deviceNumber}/calibrategratingangle")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AlpacaResponse<float>))]
    public IActionResult CalibrateGratingAngle(int deviceNumber, uint clientId, uint clientTransactionId, [FromBody] float value)
    {
        return Ok(Execute(() => _usisDevice.CalibrateGratingAngle(value), deviceNumber, clientId, clientTransactionId));
    }
    
    [HttpGet]
    [Route("{deviceNumber}/gratinganglemax")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AlpacaResponse<float>))]
    public IActionResult GetGratingAngleMax(int deviceNumber, uint clientId, uint clientTransactionId)
    {
        return Ok(Execute(() => _usisDevice.GetGratingAngleMax(), deviceNumber, clientId, clientTransactionId));
    }
    
    [HttpGet]
    [Route("{deviceNumber}/gratinganglemin")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AlpacaResponse<float>))]
    public IActionResult GetGratingAngleMin(int deviceNumber, uint clientId, uint clientTransactionId)
    {
        return Ok(Execute(() => _usisDevice.GetGratingAngleMin(), deviceNumber, clientId, clientTransactionId));
    }
    
    [HttpGet]
    [Route("{deviceNumber}/gratingangleprec")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AlpacaResponse<float>))]
    public IActionResult GetGratingAnglePrec(int deviceNumber, uint clientId, uint clientTransactionId)
    {
        return Ok(Execute(() => _usisDevice.GetGratingAnglePrec(), deviceNumber, clientId, clientTransactionId));
    }
    
    [HttpGet]
    [Route("{deviceNumber}/gratingwavelength")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AlpacaResponse<float>))]
    public IActionResult GetGratingWaveLength(int deviceNumber, uint clientId, uint clientTransactionId)
    {
        return Ok(Execute(() => _usisDevice.GetGratingWaveLength(), deviceNumber, clientId, clientTransactionId));
    }
    
    [HttpPut]
    [Route("{deviceNumber}/gratingwavelength")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AlpacaResponse<float>))]
    public IActionResult SetGratingWaveLength(int deviceNumber, uint clientId, uint clientTransactionId, [FromBody] float value)
    {
        return Ok(Execute(() => _usisDevice.SetGratingWaveLength(value), deviceNumber, clientId, clientTransactionId));
    }
    
    [HttpPut]
    [Route("{deviceNumber}/stopgratingwavelength")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AlpacaResponse<float>))]
    public IActionResult StopGratingWaveLength(int deviceNumber, uint clientId, uint clientTransactionId)
    {
        return Ok(Execute(() => _usisDevice.StopGratingWaveLength(), deviceNumber, clientId, clientTransactionId));
    }
    
    [HttpPut]
    [Route("{deviceNumber}/calibrategratingwavelength")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AlpacaResponse<float>))]
    public IActionResult CalibrateGratingWaveLength(int deviceNumber, uint clientId, uint clientTransactionId, [FromBody] float value)
    {
        return Ok(Execute(() => _usisDevice.CalibrateGratingWaveLength(value), deviceNumber, clientId, clientTransactionId));
    }
    
    [HttpGet]
    [Route("{deviceNumber}/gratingdensity")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AlpacaResponse<float>))]
    public IActionResult GetGratingDensity(int deviceNumber, uint clientId, uint clientTransactionId)
    {
        return Ok(Execute(() => _usisDevice.GetGratingDensity(), deviceNumber, clientId, clientTransactionId));
    }
    
    [HttpPut]
    [Route("{deviceNumber}/gratingdensity")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AlpacaResponse<float>))]
    public IActionResult SetGratingDensity(int deviceNumber, uint clientId, uint clientTransactionId, [FromBody] float value)
    {
        return Ok(Execute(() => _usisDevice.SetGratingDensity(value), deviceNumber, clientId, clientTransactionId));
    }
    
    #endregion
    
    #region Slit
    
    [HttpGet]
    [Route("{deviceNumber}/slitid")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AlpacaResponse<string>))]
    public IActionResult GetSlitId(int deviceNumber, uint clientId, uint clientTransactionId)
    {
        return Ok(Execute(() => _usisDevice.GetSlitId(), deviceNumber, clientId, clientTransactionId));
    }
    
    [HttpPut]
    [Route("{deviceNumber}/slitid")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AlpacaResponse<string>))]
    public IActionResult SetSlitId(int deviceNumber, uint clientId, uint clientTransactionId, [FromBody] string value)
    {
        return Ok(Execute(() => _usisDevice.SetSlitId(value), deviceNumber, clientId, clientTransactionId));
    }
    
    [HttpPut]
    [Route("{deviceNumber}/stopslitid")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AlpacaResponse<string>))]
    public IActionResult StopSlitId(int deviceNumber, uint clientId, uint clientTransactionId)
    {
        return Ok(Execute(() => _usisDevice.StopSlitId(), deviceNumber, clientId, clientTransactionId));
    }
    
    [HttpPut]
    [Route("{deviceNumber}/calibrateslitid")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AlpacaResponse<string>))]
    public IActionResult CalibrateSlitId(int deviceNumber, uint clientId, uint clientTransactionId, [FromBody] string value)
    {
        return Ok(Execute(() => _usisDevice.CalibrateSlitId(value), deviceNumber, clientId, clientTransactionId));
    }
    
    [HttpGet]
    [Route("{deviceNumber}/slitwidth")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AlpacaResponse<float>))]
    public IActionResult GetSlitWidth(int deviceNumber, uint clientId, uint clientTransactionId)
    {
        return Ok(Execute(() => _usisDevice.GetSlitWidth(), deviceNumber, clientId, clientTransactionId));
    }
    
    [HttpPut]
    [Route("{deviceNumber}/slitwidth")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AlpacaResponse<float>))]
    public IActionResult SetSlitWidth(int deviceNumber, uint clientId, uint clientTransactionId, [FromBody] float value)
    {
        return Ok(Execute(() => _usisDevice.SetSlitWidth(value), deviceNumber, clientId, clientTransactionId));
    }
    
    [HttpPut]
    [Route("{deviceNumber}/stopslitwidth")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AlpacaResponse<float>))]
    public IActionResult StopSlitWidth(int deviceNumber, uint clientId, uint clientTransactionId)
    {
        return Ok(Execute(() => _usisDevice.StopSlitWidth(), deviceNumber, clientId, clientTransactionId));
    }
    
    [HttpGet]
    [Route("{deviceNumber}/slitangle")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AlpacaResponse<float>))]
    public IActionResult GetSlitAngle(int deviceNumber, uint clientId, uint clientTransactionId)
    {
        return Ok(Execute(() => _usisDevice.GetSlitAngle(), deviceNumber, clientId, clientTransactionId));
    }
    
    [HttpPut]
    [Route("{deviceNumber}/slitangle")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AlpacaResponse<float>))]
    public IActionResult SetSlitAngle(int deviceNumber, uint clientId, uint clientTransactionId, [FromBody] float value)
    {
        return Ok(Execute(() => _usisDevice.SetSlitAngle(value), deviceNumber, clientId, clientTransactionId));
    }
    
    [HttpPut]
    [Route("{deviceNumber}/stopslitangle")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AlpacaResponse<float>))]
    public IActionResult StopSlitAngle(int deviceNumber, uint clientId, uint clientTransactionId)
    {
        return Ok(Execute(() => _usisDevice.StopSlitAngle(), deviceNumber, clientId, clientTransactionId));
    }
    
    #endregion
    
    #region Focus
    
    [HttpGet]
    [Route("{deviceNumber}/focusposition")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AlpacaResponse<string>))]
    public IActionResult GetFocusPosition(int deviceNumber, uint clientId, uint clientTransactionId)
    {
        return Ok(Execute(() => _usisDevice.GetFocusPosition(), deviceNumber, clientId, clientTransactionId));
    }
    
    [HttpPut]
    [Route("{deviceNumber}/focusposition")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AlpacaResponse<string>))]
    public IActionResult SetFocusPosition(int deviceNumber, uint clientId, uint clientTransactionId, [FromBody] float value)
    {
        return Ok(Execute(() => _usisDevice.SetFocusPosition(value), deviceNumber, clientId, clientTransactionId));
    }
    
    [HttpPut]
    [Route("{deviceNumber}/stopfocusposition")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AlpacaResponse<string>))]
    public IActionResult StopFocusPosition(int deviceNumber, uint clientId, uint clientTransactionId)
    {
        return Ok(Execute(() => _usisDevice.StopFocusPosition(), deviceNumber, clientId, clientTransactionId));
    }
    
    [HttpGet]
    [Route("{deviceNumber}/focuspositionmax")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AlpacaResponse<float>))]
    public IActionResult GetFocusPositionMax(int deviceNumber, uint clientId, uint clientTransactionId)
    {
        return Ok(Execute(() => _usisDevice.GetFocusPositionMax(), deviceNumber, clientId, clientTransactionId));
    }
    
    [HttpGet]
    [Route("{deviceNumber}/focuspositionmin")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AlpacaResponse<float>))]
    public IActionResult GetFocusPositionMin(int deviceNumber, uint clientId, uint clientTransactionId)
    {
        return Ok(Execute(() => _usisDevice.GetFocusPositionMin(), deviceNumber, clientId, clientTransactionId));
    }
    
    [HttpGet]
    [Route("{deviceNumber}/focuspositionprec")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AlpacaResponse<float>))]
    public IActionResult GetFocusPositionPrec(int deviceNumber, uint clientId, uint clientTransactionId)
    {
        return Ok(Execute(() => _usisDevice.GetFocusPositionPrec(), deviceNumber, clientId, clientTransactionId));
    }
    
    #endregion
    
    #region Light source
    
    [HttpGet]
    [Route("{deviceNumber}/lightsource")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AlpacaResponse<string>))]
    public IActionResult GetLightSource(int deviceNumber, uint clientId, uint clientTransactionId)
    {
        return Ok(Execute(() => _usisDevice.GetLightSource(), deviceNumber, clientId, clientTransactionId));
    }
    
    [HttpPut]
    [Route("{deviceNumber}/lightsource")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AlpacaResponse<string>))]
    public IActionResult GetLightSource(int deviceNumber, uint clientId, uint clientTransactionId, [FromBody] string value)
    {
        return Ok(Execute(() => _usisDevice.SetLightSource(value), deviceNumber, clientId, clientTransactionId));
    }
    
    #endregion

    private AlpacaResponse<T> Execute<T>(Func<IResponse<T>> usisCommand, int deviceNumber, uint clientId, uint clientTransactionId)
    {
        uint serverTransactionId = _serverTransactionIdProvider.GetServerTransactionId();
        using IDisposable? scope = _logger.BeginScope(deviceNumber, clientId, clientTransactionId, serverTransactionId);
        
        try
        {
            IResponse<T> response = usisCommand();
            AlpacaResponse<T> alpacaResponse = AlpacaResponseBuilder.BuildAlpacaResponse(clientTransactionId, serverTransactionId, response);
            return alpacaResponse;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while executing USIS command : {ExceptionMessage}", e.Message);
            return AlpacaResponseBuilder.BuildAlpacaResponse<T>(clientTransactionId, serverTransactionId, e);
        }
    }
}