using NSubstitute;
using Shelyak.Usis;
using Shelyak.Usis.Commands;
using Shelyak.Usis.Enums;
using Shelyak.Usis.Responses;

namespace Shelyak.Isis.Test;

public class UsisDeviceTest
{
    private readonly ICommandSender _mockCommandSender;
    private readonly IUsisDevice _usisDevice;

    public UsisDeviceTest()
    {
        _mockCommandSender = Substitute.For<ICommandSender>();
        var mockResponser = Substitute.For<IResponseParser>();
        _usisDevice = new UsisDevice(_mockCommandSender, mockResponser);
    }

    [Fact]
    public void GetDeviceName_SendsCorrectCommand()
    {
        // Arrange
        var expectedCommand = new GetCommand(DeviceProperty.DEVICE_NAME, PropertyAttributeType.VALUE);

        // Act
        _ = _usisDevice.GetDeviceName();

        // Assert
        _mockCommandSender.Received().SendCommand(Arg.Is<ICommand>(command => command.Build() == expectedCommand.Build()));
    }

    [Fact]
    public void GetSoftwareVersion_SendsCorrectCommand()
    {
        // Arrange
        var expectedCommand = new GetCommand(DeviceProperty.SOFTWARE_VERSION, PropertyAttributeType.VALUE);

        // Act
        _ = _usisDevice.GetSoftwareVersion();

        // Assert
        _mockCommandSender.Received().SendCommand(Arg.Is<ICommand>(command => command.Build() == expectedCommand.Build()));
    }

    [Fact]
    public void GetProtocolVersion_SendsCorrectCommand()
    {
        // Arrange
        var expectedCommand = new GetCommand(DeviceProperty.PROTOCOL_VERSION, PropertyAttributeType.VALUE);

        // Act
        _ = _usisDevice.GetProtocolVersion();

        // Assert
        _mockCommandSender.Received().SendCommand(Arg.Is<ICommand>(command => command.Build() == expectedCommand.Build()));
    }

    [Fact]
    public void GetTemperature_SendsCorrectCommand()
    {
        // Arrange
        var expectedCommand = new GetCommand(DeviceProperty.TEMPERATURE, PropertyAttributeType.VALUE);

        // Act
        _ = _usisDevice.GetTemperature();

        // Assert
        _mockCommandSender.Received().SendCommand(Arg.Is<ICommand>(command => command.Build() == expectedCommand.Build()));
    }

    [Fact]
    public void GetHumidity_SendsCorrectCommand()
    {
        // Arrange
        var expectedCommand = new GetCommand(DeviceProperty.HUMIDITY, PropertyAttributeType.VALUE);

        // Act
        _ = _usisDevice.GetHumidity();

        // Assert
        _mockCommandSender.Received().SendCommand(Arg.Is<ICommand>(command => command.Build() == expectedCommand.Build()));
    }

    [Fact]
    public void GetGratingId_SendsCorrectCommand()
    {
        // Arrange
        var expectedCommand = new GetCommand(DeviceProperty.GRATING_ID, PropertyAttributeType.VALUE);

        // Act
        _ = _usisDevice.GetGratingId();

        // Assert
        _mockCommandSender.Received().SendCommand(Arg.Is<ICommand>(command => command.Build() == expectedCommand.Build()));
    }

    [Fact]
    public void SetGratingId_SendsCorrectCommand()
    {
        // Arrange
        var expectedCommand = new SetCommand<string>(DeviceProperty.GRATING_ID, PropertyAttributeType.VALUE, "1");

        // Act
        _ = _usisDevice.SetGratingId("1");

        // Assert
        _mockCommandSender.Received().SendCommand(Arg.Is<ICommand>(command => command.Build() == expectedCommand.Build()));
    }

    [Fact]
    public void GetGratingAngle_SendsCorrectCommand()
    {
        // Arrange
        var expectedCommand = new GetCommand(DeviceProperty.GRATING_ANGLE, PropertyAttributeType.VALUE);

        // Act
        _ = _usisDevice.GetGratingAngle();

        // Assert
        _mockCommandSender.Received().SendCommand(Arg.Is<ICommand>(command => command.Build() == expectedCommand.Build()));
    }

    [Fact]
    public void SetGratingAngle_SendsCorrectCommand()
    {
        // Arrange
        var expectedCommand = new SetCommand<float>(DeviceProperty.GRATING_ANGLE, PropertyAttributeType.VALUE, 1.0f);

        // Act
        _ = _usisDevice.SetGratingAngle(1.0f);

        // Assert
        _mockCommandSender.Received().SendCommand(Arg.Is<ICommand>(command => command.Build() == expectedCommand.Build()));
    }

    [Fact]
    public void StopGratingAngle_SendsCorrectCommand()
    {
        // Arrange
        var expectedCommand = new StopCommand<float>(DeviceProperty.GRATING_ANGLE);

        // Act
        _ = _usisDevice.StopGratingAngle();

        // Assert
        _mockCommandSender.Received().SendCommand(Arg.Is<ICommand>(command => command.Build() == expectedCommand.Build()));
    }

    [Fact]
    public void CalibrateGratingAngle_SendsCorrectCommand()
    {
        // Arrange
        var expectedCommand = new CalibrateCommand<float>(DeviceProperty.GRATING_ANGLE, 1.0f);

        // Act
        _ = _usisDevice.CalibrateGratingAngle(1.0f);

        // Assert
        _mockCommandSender.Received().SendCommand(Arg.Is<ICommand>(command => command.Build() == expectedCommand.Build()));
    }

    [Fact]
    public void GetGratingAngleMax_SendsCorrectCommand()
    {
        // Arrange
        var expectedCommand = new GetCommand(DeviceProperty.GRATING_ANGLE_MAX, PropertyAttributeType.MAX);

        // Act
        _ = _usisDevice.GetGratingAngleMax();

        // Assert
        _mockCommandSender.Received().SendCommand(Arg.Is<ICommand>(command => command.Build() == expectedCommand.Build()));
    }

    [Fact]
    public void GetGratingAngleMin_SendsCorrectCommand()
    {
        // Arrange
        var expectedCommand = new GetCommand(DeviceProperty.GRATING_ANGLE_MIN, PropertyAttributeType.MIN);

        // Act
        _ = _usisDevice.GetGratingAngleMin();

        // Assert
        _mockCommandSender.Received().SendCommand(Arg.Is<ICommand>(command => command.Build() == expectedCommand.Build()));
    }

    [Fact]
    public void GetGratingAnglePrec_SendsCorrectCommand()
    {
        // Arrange
        var expectedCommand = new GetCommand(DeviceProperty.GRATING_ANGLE_PREC, PropertyAttributeType.PREC);

        // Act
        _ = _usisDevice.GetGratingAnglePrec();

        // Assert
        _mockCommandSender.Received().SendCommand(Arg.Is<ICommand>(command => command.Build() == expectedCommand.Build()));
    }

    [Fact]
    public void GetGratingWaveLength_SendsCorrectCommand()
    {
        // Arrange
        var expectedCommand = new GetCommand(DeviceProperty.GRATING_WAVELENGTH, PropertyAttributeType.VALUE);

        // Act
        _ = _usisDevice.GetGratingWaveLength();

        // Assert
        _mockCommandSender.Received().SendCommand(Arg.Is<ICommand>(command => command.Build() == expectedCommand.Build()));
    }

    [Fact]
    public void SetGratingWaveLength_SendsCorrectCommand()
    {
        // Arrange
        var expectedCommand = new SetCommand<float>(DeviceProperty.GRATING_WAVELENGTH, PropertyAttributeType.VALUE, 1.0f);

        // Act
        _ = _usisDevice.SetGratingWaveLength(1.0f);

        // Assert
        _mockCommandSender.Received().SendCommand(Arg.Is<ICommand>(command => command.Build() == expectedCommand.Build()));
    }

    [Fact]
    public void StopGratingWaveLength_SendsCorrectCommand()
    {
        // Arrange
        var expectedCommand = new StopCommand<float>(DeviceProperty.GRATING_WAVELENGTH);

        // Act
        _ = _usisDevice.StopGratingWaveLength();

        // Assert
        _mockCommandSender.Received().SendCommand(Arg.Is<ICommand>(command => command.Build() == expectedCommand.Build()));
    }

    [Fact]
    public void CalibrateGratingWaveLength_SendsCorrectCommand()
    {
        // Arrange
        var expectedCommand = new CalibrateCommand<float>(DeviceProperty.GRATING_WAVELENGTH, 1.0f);

        // Act
        _ = _usisDevice.CalibrateGratingWaveLength(1.0f);

        // Assert
        _mockCommandSender.Received().SendCommand(Arg.Is<ICommand>(command => command.Build() == expectedCommand.Build()));
    }

    [Fact]
    public void GetGratingWaveLengthMax_SendsCorrectCommand()
    {
        // Arrange
        var expectedCommand = new GetCommand(DeviceProperty.GRATING_WAVELENGTH_MAX, PropertyAttributeType.MAX);

        // Act
        _ = _usisDevice.GetGratingWaveLengthMax();

        // Assert
        _mockCommandSender.Received().SendCommand(Arg.Is<ICommand>(command => command.Build() == expectedCommand.Build()));
    }

    [Fact]
    public void GetGratingWaveLengthMin_SendsCorrectCommand()
    {
        // Arrange
        var expectedCommand = new GetCommand(DeviceProperty.GRATING_WAVELENGTH_MIN, PropertyAttributeType.MIN);

        // Act
        _ = _usisDevice.GetGratingWaveLengthMin();

        // Assert
        _mockCommandSender.Received().SendCommand(Arg.Is<ICommand>(command => command.Build() == expectedCommand.Build()));
    }

    [Fact]
    public void GetGratingWaveLengthPrec_SendsCorrectCommand()
    {
        // Arrange
        var expectedCommand = new GetCommand(DeviceProperty.GRATING_WAVELENGTH_PREC, PropertyAttributeType.PREC);

        // Act
        _ = _usisDevice.GetGratingWaveLengthPrec();

        // Assert
        _mockCommandSender.Received().SendCommand(Arg.Is<ICommand>(command => command.Build() == expectedCommand.Build()));
    }

    [Fact]
    public void GetGratingDensity_SendsCorrectCommand()
    {
        // Arrange
        var expectedCommand = new GetCommand(DeviceProperty.GRATING_DENSITY, PropertyAttributeType.VALUE);

        // Act
        _ = _usisDevice.GetGratingDensity();

        // Assert
        _mockCommandSender.Received().SendCommand(Arg.Is<ICommand>(command => command.Build() == expectedCommand.Build()));
    }

    [Fact]
    public void SetGratingDensity_SendsCorrectCommand()
    {
        // Arrange
        var expectedCommand = new SetCommand<float>(DeviceProperty.GRATING_DENSITY, PropertyAttributeType.VALUE, 1.0f);

        // Act
        _ = _usisDevice.SetGratingDensity(1.0f);

        // Assert
        _mockCommandSender.Received().SendCommand(Arg.Is<ICommand>(command => command.Build() == expectedCommand.Build()));
    }

    [Fact]
    public void GetSlitId_SendsCorrectCommand()
    {
        // Arrange
        var expectedCommand = new GetCommand(DeviceProperty.SLIT_ID, PropertyAttributeType.VALUE);

        // Act
        _ = _usisDevice.GetSlitId();

        // Assert
        _mockCommandSender.Received().SendCommand(Arg.Is<ICommand>(command => command.Build() == expectedCommand.Build()));
    }

    [Fact]
    public void SetSlitId_SendsCorrectCommand()
    {
        // Arrange
        var expectedCommand = new SetCommand<string>(DeviceProperty.SLIT_ID, PropertyAttributeType.VALUE, "1");

        // Act
        _ = _usisDevice.SetSlitId("1");

        // Assert
        _mockCommandSender.Received().SendCommand(Arg.Is<ICommand>(command => command.Build() == expectedCommand.Build()));
    }

    [Fact]
    public void GetSlitWidth_SendsCorrectCommand()
    {
        // Arrange
        var expectedCommand = new GetCommand(DeviceProperty.SLIT_WIDTH, PropertyAttributeType.VALUE);

        // Act
        _ = _usisDevice.GetSlitWidth();

        // Assert
        _mockCommandSender.Received().SendCommand(Arg.Is<ICommand>(command => command.Build() == expectedCommand.Build()));
    }

    [Fact]
    public void SetSlitWidth_SendsCorrectCommand()
    {
        // Arrange
        var expectedCommand = new SetCommand<float>(DeviceProperty.SLIT_WIDTH, PropertyAttributeType.VALUE, 1.0f);

        // Act
        _ = _usisDevice.SetSlitWidth(1.0f);

        // Assert
        _mockCommandSender.Received().SendCommand(Arg.Is<ICommand>(command => command.Build() == expectedCommand.Build()));
    }

    [Fact]
    public void GetSlitAngle_SendsCorrectCommand()
    {
        // Arrange
        var expectedCommand = new GetCommand(DeviceProperty.SLIT_ANGLE, PropertyAttributeType.VALUE);

        // Act
        _ = _usisDevice.GetSlitAngle();

        // Assert
        _mockCommandSender.Received().SendCommand(Arg.Is<ICommand>(command => command.Build() == expectedCommand.Build()));
    }

    [Fact]
    public void SetSlitAngle_SendsCorrectCommand()
    {
        // Arrange
        var expectedCommand = new SetCommand<float>(DeviceProperty.SLIT_ANGLE, PropertyAttributeType.VALUE, 1.0f);

        // Act
        _ = _usisDevice.SetSlitAngle(1.0f);

        // Assert
        _mockCommandSender.Received().SendCommand(Arg.Is<ICommand>(command => command.Build() == expectedCommand.Build()));
    }

    [Fact]
    public void StopSlitAngle_SendsCorrectCommand()
    {
        // Arrange
        var expectedCommand = new StopCommand<float>(DeviceProperty.SLIT_ANGLE);

        // Act
        _ = _usisDevice.StopSlitAngle();

        // Assert
        _mockCommandSender.Received().SendCommand(Arg.Is<ICommand>(command => command.Build() == expectedCommand.Build()));
    }

    [Fact]
    public void CalibrateSlitAngle_SendsCorrectCommand()
    {
        // Arrange
        var expectedCommand = new CalibrateCommand<float>(DeviceProperty.SLIT_ANGLE, 1.0f);

        // Act
        _ = _usisDevice.CalibrateSlitAngle(1.0f);

        // Assert
        _mockCommandSender.Received().SendCommand(Arg.Is<ICommand>(command => command.Build() == expectedCommand.Build()));
    }

    [Fact]
    public void GetFocusPosition_SendsCorrectCommand()
    {
        // Arrange
        var expectedCommand = new GetCommand(DeviceProperty.FOCUS_POSITION, PropertyAttributeType.VALUE);
        
        // Act
        _ = _usisDevice.GetFocusPosition();

        // Assert
        _mockCommandSender.Received().SendCommand(Arg.Is<ICommand>(command => command.Build() == expectedCommand.Build()));
    }
    
    [Fact]
    public void SetFocusPosition_SendsCorrectCommand()
    {
        // Arrange
        var expectedCommand = new SetCommand<float>(DeviceProperty.FOCUS_POSITION, PropertyAttributeType.VALUE, 1.0f);
        
        // Act
        _ = _usisDevice.SetFocusPosition(1.0f);

        // Assert
        _mockCommandSender.Received().SendCommand(Arg.Is<ICommand>(command => command.Build() == expectedCommand.Build()));
    }
    
    [Fact]
    public void StopFocusPosition_SendsCorrectCommand()
    {
        // Arrange
        var expectedCommand = new StopCommand<float>(DeviceProperty.FOCUS_POSITION);
        
        // Act
        _ = _usisDevice.StopFocusPosition();

        // Assert
        _mockCommandSender.Received().SendCommand(Arg.Is<ICommand>(command => command.Build() == expectedCommand.Build()));
    }
    
    [Fact]
    public void CalibrateFocusPosition_SendsCorrectCommand()
    {
        // Arrange
        var expectedCommand = new CalibrateCommand<float>(DeviceProperty.FOCUS_POSITION, 1.0f);
        
        // Act
        _ = _usisDevice.CalibrateFocusPosition(1.0f);

        // Assert
        _mockCommandSender.Received().SendCommand(Arg.Is<ICommand>(command => command.Build() == expectedCommand.Build()));
    }
    
    [Fact]
    public void GetFocusPositionMax_SendsCorrectCommand()
    {
        // Arrange
        var expectedCommand = new GetCommand(DeviceProperty.FOCUS_POSITION_MAX, PropertyAttributeType.MAX);
        
        // Act
        _ = _usisDevice.GetFocusPositionMax();

        // Assert
        _mockCommandSender.Received().SendCommand(Arg.Is<ICommand>(command => command.Build() == expectedCommand.Build()));
    }
    
    [Fact]
    public void GetFocusPositionMin_SendsCorrectCommand()
    {
        // Arrange
        var expectedCommand = new GetCommand(DeviceProperty.FOCUS_POSITION_MIN, PropertyAttributeType.MIN);
        
        // Act
        _ = _usisDevice.GetFocusPositionMin();

        // Assert
        _mockCommandSender.Received().SendCommand(Arg.Is<ICommand>(command => command.Build() == expectedCommand.Build()));
    }
    
    [Fact]
    public void GetFocusPositionPrec_SendsCorrectCommand()
    {
        // Arrange
        var expectedCommand = new GetCommand(DeviceProperty.FOCUS_POSITION_PREC, PropertyAttributeType.PREC);
        
        // Act
        _ = _usisDevice.GetFocusPositionPrec();

        // Assert
        _mockCommandSender.Received().SendCommand(Arg.Is<ICommand>(command => command.Build() == expectedCommand.Build()));
    }
    
    [Fact]
    public void GetLightSource_SendsCorrectCommand()
    {
        // Arrange
        var expectedCommand = new GetCommand(DeviceProperty.LIGHT_SOURCE, PropertyAttributeType.VALUE);
        
        // Act
        _ = _usisDevice.GetLightSource();

        // Assert
        _mockCommandSender.Received().SendCommand(Arg.Is<ICommand>(command => command.Build() == expectedCommand.Build()));
    }
    
    [Fact]
    public void SetLightSource_SendsCorrectCommand()
    {
        // Arrange
        var expectedCommand = new SetCommandEnum<LightSource>(DeviceProperty.LIGHT_SOURCE, PropertyAttributeType.VALUE, LightSource.FLAT);
        
        // Act
        _ = _usisDevice.SetLightSource(LightSource.FLAT);

        // Assert
        _mockCommandSender.Received().SendCommand(Arg.Is<ICommand>(command => command.Build() == expectedCommand.Build()));
    }
}