using Shelyak.Usis.Enums;

namespace Shelyak.Usis.Commands;

public class CommandFactory : ICommandFactory
{
    public ICommand CreateCommand<T>(CommandType commandType, DeviceProperty deviceProperty, PropertyAttributeType attributeType)
    {
        return CreateCommand(commandType, deviceProperty, attributeType, 0);
    }
        
    public ICommand CreateCommand<T>(CommandType commandType, DeviceProperty deviceProperty, PropertyAttributeType attributeType, T value)
    {
        if(commandType == CommandType.GET)
        {
            return new GetCommand(deviceProperty, attributeType);
        }
        else if (commandType == CommandType.SET)
        {
            return new SetCommand<T>(deviceProperty, attributeType, value);
        }

        throw new InvalidOperationException();
    }
}