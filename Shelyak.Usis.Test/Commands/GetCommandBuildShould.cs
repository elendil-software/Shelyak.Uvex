using Shelyak.Usis.Commands;
using Shelyak.Usis.Enums;

namespace Shelyak.Isis.Test.Commands;

public class GetCommandBuildShould
{
    [Theory]
    [InlineData(DeviceProperty.GRATING_ANGLE, PropertyAttributeType.VALUE, "GET;GRATING_ANGLE;VALUE")]
    public void ReturnExpectedCommand(DeviceProperty deviceProperty, PropertyAttributeType attributeType, string expectedCommand)
    {
        var command = new GetCommand(deviceProperty, attributeType);
        string actual = command.Build();
        Assert.Equal(expectedCommand, actual);
    }
}