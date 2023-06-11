using Microsoft.AspNetCore.Mvc;
using Shelyak.Usis.Commands;
using Shelyak.Usis.Enums;
using Shelyak.Usis.Responses;
using Shelyak.Uvex.WebApi.Alpaca;

namespace Shelyak.Uvex.WebApi.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class SpectrographController : ControllerBase
{
    
    private readonly IServerTransactionIdProvider _serverTransactionIdProvider;
    private readonly ILogger<SpectrographController> _logger;
    private readonly ICommandFacade _commandFacade;

    public SpectrographController(ICommandFacade commandFacade, IServerTransactionIdProvider serverTransactionIdProvider, ILogger<SpectrographController> logger)
    {
        _commandFacade = commandFacade;
        _serverTransactionIdProvider = serverTransactionIdProvider;
        _logger = logger;
    }

    #region Grating
    
    [HttpGet]
    [Route("{deviceNumber}/gratingid")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AlpacaResponse<float>))]
    public IActionResult GetGratingId(int deviceNumber, uint clientId, uint clientTransactionId)
    {
        return Ok(Execute<float>(CommandType.GET, DeviceProperty.GRATING_ID, PropertyAttributeType.VALUE, deviceNumber, clientId, clientTransactionId));
    }

    [HttpGet]
    [Route("{deviceNumber}/gratingangle")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AlpacaResponse<float>))]
    public IActionResult GetGratingAngle(int deviceNumber, uint clientId, uint clientTransactionId)
    {
        return Ok(Execute<float>(CommandType.GET, DeviceProperty.GRATING_ANGLE, PropertyAttributeType.VALUE, deviceNumber, clientId, clientTransactionId));
    }
    
    [HttpPut]
    [Route("{deviceNumber}/gratingangle")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AlpacaResponse<float>))]
    public IActionResult SetGratingAngle(int deviceNumber, uint clientId, uint clientTransactionId, [FromBody] float value)
    {
        return Ok(Execute(CommandType.SET, DeviceProperty.GRATING_ANGLE, PropertyAttributeType.VALUE, deviceNumber, clientId, clientTransactionId, value));
    }
    
    [HttpGet]
    [Route("{deviceNumber}/gratingwavelength")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AlpacaResponse<float>))]
    public IActionResult GetGratingWaveLength(int deviceNumber, uint clientId, uint clientTransactionId)
    {
        return Ok(Execute<float>(CommandType.GET, DeviceProperty.GRATING_WAVELENGTH, PropertyAttributeType.VALUE, deviceNumber, clientId, clientTransactionId));
    }
    
    [HttpPut]
    [Route("{deviceNumber}/gratingwavelength")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AlpacaResponse<float>))]
    public IActionResult SetGratingWaveLength(int deviceNumber, uint clientId, uint clientTransactionId, [FromBody] float value)
    {
        return Ok(Execute(CommandType.SET, DeviceProperty.GRATING_WAVELENGTH, PropertyAttributeType.VALUE, deviceNumber, clientId, clientTransactionId, value));
    }
    
    [HttpGet]
    [Route("{deviceNumber}/gratingdensity")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AlpacaResponse<float>))]
    public IActionResult GetGratingDensity(int deviceNumber, uint clientId, uint clientTransactionId)
    {
        return Ok(Execute<float>(CommandType.GET, DeviceProperty.GRATING_DENSITY, PropertyAttributeType.VALUE, deviceNumber, clientId, clientTransactionId));
    }
    
    #endregion

    #region Slit

    [HttpGet]
    [Route("{deviceNumber}/slitid")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AlpacaResponse<string>))]
    public IActionResult GetSlitId(int deviceNumber, uint clientId, uint clientTransactionId)
    {
        return Ok(Execute<string>(CommandType.GET, DeviceProperty.SLIT_ID, PropertyAttributeType.VALUE, deviceNumber, clientId, clientTransactionId));
    }
    
    [HttpGet]
    [Route("{deviceNumber}/slitwidth")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AlpacaResponse<float>))]
    public IActionResult GetSlitWidth(int deviceNumber, uint clientId, uint clientTransactionId)
    {
        return Ok(Execute<float>(CommandType.GET, DeviceProperty.SLIT_WIDTH, PropertyAttributeType.VALUE, deviceNumber, clientId, clientTransactionId));
    }
    
    [HttpGet]
    [Route("{deviceNumber}/slitangle")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AlpacaResponse<float>))]
    public IActionResult GetSlitAngle(int deviceNumber, uint clientId, uint clientTransactionId)
    {
        return Ok(Execute<float>(CommandType.GET, DeviceProperty.SLIT_ANGLE, PropertyAttributeType.VALUE, deviceNumber, clientId, clientTransactionId));
    }

    #endregion
    
    #region Focus
    
    [HttpGet]
    [Route("{deviceNumber}/focusposition")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AlpacaResponse<string>))]
    public IActionResult GetFocusPosition(int deviceNumber, uint clientId, uint clientTransactionId)
    {
        return Ok(Execute<string>(CommandType.GET, DeviceProperty.FOCUS_POSITION, PropertyAttributeType.VALUE, deviceNumber, clientId, clientTransactionId));
    }
    
    [HttpPut]
    [Route("{deviceNumber}/focusposition")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AlpacaResponse<string>))]
    public IActionResult GetFocusPosition(int deviceNumber, uint clientId, uint clientTransactionId, float value)
    {
        return Ok(Execute(CommandType.SET, DeviceProperty.FOCUS_POSITION, PropertyAttributeType.VALUE, deviceNumber, clientId, clientTransactionId, value));
    }
    
    #endregion
    
    #region Light source
    
    [HttpGet]
    [Route("{deviceNumber}/lightsource")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AlpacaResponse<string>))]
    public IActionResult GetLightSource(int deviceNumber, uint clientId, uint clientTransactionId)
    {
        return Ok(Execute<string>(CommandType.GET, DeviceProperty.LIGHT_SOURCE, PropertyAttributeType.VALUE, deviceNumber, clientId, clientTransactionId));
    }
    
    [HttpPut]
    [Route("{deviceNumber}/lightsource")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AlpacaResponse<string>))]
    public IActionResult GetLightSource(int deviceNumber, uint clientId, uint clientTransactionId, [FromBody] string value)
    {
        return Ok(Execute<string>(CommandType.SET, DeviceProperty.LIGHT_SOURCE, PropertyAttributeType.VALUE, deviceNumber, clientId, clientTransactionId, value));
    }
    
    #endregion

    private AlpacaResponse<T> Execute<T>(CommandType commandType, DeviceProperty gratingAngle, PropertyAttributeType propertyAttributeType,
        int deviceNumber, uint clientId, uint clientTransactionId, T value = default!)
    {
        uint serverTransactionId = _serverTransactionIdProvider.GetServerTransactionId();
        using IDisposable? scope = _logger.BeginScope(deviceNumber, clientId, clientTransactionId, serverTransactionId);
        
        try
        {
            _logger.LogInformation("Executing command {CommandType} {DeviceProperty} {PropertyAttributeType}", commandType, gratingAngle, propertyAttributeType);
            IResponse response = _commandFacade.ExecuteCommand(commandType, gratingAngle, propertyAttributeType, value);
            AlpacaResponse<T> alpacaResponse = AlpacaResponseBuilder.BuildAlpacaResponse<T>(clientTransactionId, serverTransactionId, response);
            _logger.LogInformation("Command {CommandType} {DeviceProperty} {PropertyAttributeType} executed successfully, Response: {@AlpacaResponse}", 
                commandType, gratingAngle, propertyAttributeType, alpacaResponse);
            return alpacaResponse;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while executing command {CommandType} {DeviceProperty} {PropertyAttributeType} : {ExceptionMessage}", 
                commandType, gratingAngle, propertyAttributeType, e.Message);
            return AlpacaResponseBuilder.BuildAlpacaResponse<T>(clientTransactionId, serverTransactionId, e);
        }
    }
}