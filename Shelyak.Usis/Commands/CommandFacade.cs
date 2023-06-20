using Shelyak.Usis.Enums;
using Shelyak.Usis.Responses;

namespace Shelyak.Usis.Commands;

public class CommandFacade : ICommandFacade
{
    private readonly ICommandSender _commandSender;
    private readonly ICommandFactory _commandFactory;
    private readonly IResponseParser _responseParser;

    public CommandFacade(ICommandSender commandSender, ICommandFactory commandFactory, IResponseParser responseParser)
    {
        _commandSender = commandSender;
        _commandFactory = commandFactory;
        _responseParser = responseParser;
    }

    public IResponse ExecuteCommand<T>(CommandType commandType, DeviceProperty gratingAngle, PropertyAttributeType propertyAttributeType, T value = default)
    {
        ICommand command = _commandFactory.CreateCommand(commandType, gratingAngle, propertyAttributeType, value);
        string responseString = _commandSender.SendCommand(command);
        IResponse response = _responseParser.Parse<T>(responseString);
        return response;
    }
}