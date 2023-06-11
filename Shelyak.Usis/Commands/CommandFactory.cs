using Shelyak.Usis.Enums;

namespace Shelyak.Usis.Commands
{
    public class CommandFactory : ICommandFactory
    {
        public ICommand CreateCommand(CommandType commandType, DeviceProperty deviceProperty, PropertyAttributeType attributeType)
        {
            if(commandType == CommandType.GET)
            {
                return new GetCommand(deviceProperty, attributeType);
            }

            throw new InvalidOperationException();
        }
    }
}