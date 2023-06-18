using Shelyak.Usis.Enums;

namespace Shelyak.Usis.Commands;

public class CommandFactory : ICommandFactory
{
    public ICommand CreateCommand<T>(CommandType commandType, DeviceProperty deviceProperty, PropertyAttributeType attributeType)
    {
        return CreateCommand(commandType, deviceProperty, attributeType, default(T));
    }
        
    public ICommand CreateCommand<T>(CommandType commandType, DeviceProperty deviceProperty, PropertyAttributeType attributeType, T value)
    {
        return commandType switch
        {
            CommandType.GET => new GetCommand(deviceProperty, attributeType),
            CommandType.SET => new SetCommand<T>(deviceProperty, attributeType, value),
            CommandType.STOP => new StopCommand<T>(deviceProperty),
            _ => throw new InvalidOperationException()
        };
    }
}