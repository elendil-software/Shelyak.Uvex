using Microsoft.AspNetCore.Mvc;
using Shelyak.Usis;
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
    private readonly ICommandSender _commandSender;
    private readonly IServerTransactionIdProvider _serverTransactionIdProvider;

    public SpectrographController(ICommandSender commandSender, IServerTransactionIdProvider serverTransactionIdProvider)
    {
        _commandSender = commandSender;
        _serverTransactionIdProvider = serverTransactionIdProvider;
    }

    [HttpGet]
    [Route("{deviceNumber}/gratingangle")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AlpacaResponse<float>))]
    public IActionResult GetGratingAngle(int deviceNumber, uint clientId, uint clientTransactionId)
    {
        AlpacaResponse<float> alpacaResponse = ExecuteCommand<float>(clientTransactionId, DeviceProperty.GRATING_ID, PropertyAttributeType.VALUE);
        return Ok(alpacaResponse);
    }

    private AlpacaResponse<T> ExecuteCommand<T>(uint clientTransactionId,DeviceProperty gratingAngle, PropertyAttributeType propertyAttributeType)
    {
        uint serverTransactionId = 0;
        
        try
        {
            serverTransactionId = _serverTransactionIdProvider.GetServerTransactionId();
            string responseString = _commandSender.SendCommand(new GetCommand(gratingAngle, propertyAttributeType));
            IResponse response = ResponseParser.Parse<T>(responseString);
            return AlpacaResponseBuilder.BuildAlpacaResponse<T>(clientTransactionId, serverTransactionId, response);
        }
        catch (Exception e)
        {
            return AlpacaResponseBuilder.BuildAlpacaResponse<T>(clientTransactionId, serverTransactionId, e);
        }
    }
}