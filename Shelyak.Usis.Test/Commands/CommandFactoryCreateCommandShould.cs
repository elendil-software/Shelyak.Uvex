using Shelyak.Usis.Commands;
using Shelyak.Usis.Enums;

namespace Shelyak.Isis.Test.Commands;

public class CommandFactoryCreateCommandShould
{
    [Theory]
    [InlineData(CommandType.GET, DeviceProperty.GRATING_ANGLE, PropertyAttributeType.VALUE, typeof(GetCommand))]
    [InlineData(CommandType.SET, DeviceProperty.GRATING_ANGLE, PropertyAttributeType.VALUE, typeof(SetCommand<float>))]
    [InlineData(CommandType.STOP, DeviceProperty.GRATING_ANGLE, null, typeof(StopCommand<float>))]
    public void ReturnExpectedCommandType(CommandType commandType, DeviceProperty deviceProperty, PropertyAttributeType attributeType, Type expectedType)
    {
        var commandFactory = new CommandFactory();
        ICommand command = commandFactory.CreateCommand<float>(commandType, deviceProperty, attributeType);
        Assert.IsType(expectedType, command);
    }
}