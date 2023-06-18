using Shelyak.Usis.Commands;
using Shelyak.Usis.Enums;

namespace Shelyak.Isis.Test.Commands;

public class SetCommandBuildShould
{
    [Theory]
    [InlineData(DeviceProperty.GRATING_ANGLE, PropertyAttributeType.VALUE, 1.5, "SET;GRATING_ANGLE;VALUE;1.5")]
    public void ReturnExpectedCommand(DeviceProperty deviceProperty, PropertyAttributeType attributeType, float value, string expectedCommand)
    {
        var command = new SetCommand<float>(deviceProperty, attributeType, value);
        string actual = command.Build();
        Assert.Equal(expectedCommand, actual);
    }
}