using Shelyak.Usis.Enums;
using Shelyak.Usis.Responses;

namespace Shelyak.Usis.Commands
{
    public class CommandFacade : ICommandFacade
    {
        private readonly ICommandSender _commandSender;
        private readonly ICommandFactory _commandFactory;

        public CommandFacade(ICommandSender commandSender, ICommandFactory commandFactory)
        {
            _commandSender = commandSender;
            _commandFactory = commandFactory;
        }

        public IResponse ExecuteCommand<T>(CommandType commandType, DeviceProperty gratingAngle, PropertyAttributeType propertyAttributeType)
        {
            ICommand command = _commandFactory.CreateCommand(commandType, gratingAngle, propertyAttributeType);
            string responseString = _commandSender.SendCommand(command);
            IResponse response = ResponseParser.Parse<T>(responseString);
            return response;
        }
    }
}