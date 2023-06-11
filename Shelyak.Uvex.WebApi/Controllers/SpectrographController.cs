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

    [HttpGet]
    [Route("{deviceNumber}/gratingangle")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AlpacaResponse<float>))]
    public IActionResult GetGratingAngle(int deviceNumber, uint clientId, uint clientTransactionId)
    {
        return Ok(Execute<float>(
            CommandType.GET, DeviceProperty.GRATING_ANGLE, PropertyAttributeType.VALUE, 
            deviceNumber, clientId, clientTransactionId));
    }

    private AlpacaResponse<T> Execute<T>(CommandType commandType, DeviceProperty gratingAngle, PropertyAttributeType propertyAttributeType,
        int deviceNumber, uint clientId, uint clientTransactionId)
    {
        uint serverTransactionId = _serverTransactionIdProvider.GetServerTransactionId();
        using IDisposable? scope = _logger.BeginScope(deviceNumber, clientId, clientTransactionId, serverTransactionId);
        
        try
        {
            _logger.LogInformation("Executing command {CommandType} {DeviceProperty} {PropertyAttributeType}", commandType, gratingAngle, propertyAttributeType);
            IResponse response = _commandFacade.ExecuteCommand<T>(commandType, gratingAngle, propertyAttributeType);
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