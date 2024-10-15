using Shelyak.Usis.Commands;
using Shelyak.Usis.Enums;

namespace Shelyak.Isis.Test.Commands;

public class StopCommandBuildShould
{
    [Theory]
    [InlineData(DeviceProperty.GRATING_ANGLE, "STOP;GRATING_ANGLE")]
    public void ReturnExpectedCommand(DeviceProperty deviceProperty, string expectedCommand)
    {
        var command = new StopCommand(deviceProperty);
        string actual = command.Build();
        Assert.Equal(expectedCommand, actual);
    }
}