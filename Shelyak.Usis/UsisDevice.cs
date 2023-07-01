using Shelyak.Usis.Commands;
using Shelyak.Usis.Enums;
using Shelyak.Usis.Responses;

namespace Shelyak.Usis;

public class UsisDevice : IUsisDevice
{
    private readonly ICommandSender _commandSender;
    private readonly IResponseParser _responseParser;
    
    public UsisDevice(ICommandSender commandSender, IResponseParser responseParser)
    {
        _commandSender = commandSender;
        _responseParser = responseParser;
    }
    
    #region Device
    
    public IResponse<string> GetDeviceName()
    {
        ICommand command = new GetCommand(DeviceProperty.DEVICE_NAME, PropertyAttributeType.VALUE);
        string response = _commandSender.SendCommand(command);
        return _responseParser.Parse<string>(response);
    }

    public IResponse<string> GetSoftwareVersion()
    {
        ICommand command = new GetCommand(DeviceProperty.SOFTWARE_VERSION, PropertyAttributeType.VALUE);
        string response = _commandSender.SendCommand(command);
        return _responseParser.Parse<string>(response);
    }

    public IResponse<string> GetProtocolVersion()
    {
        ICommand command = new GetCommand(DeviceProperty.PROTOCOL_VERSION, PropertyAttributeType.VALUE);
        string response = _commandSender.SendCommand(command);
        return _responseParser.Parse<string>(response);
    }

    public IResponse<string> GetTemperature()
    {
        ICommand command = new GetCommand(DeviceProperty.TEMPERATURE, PropertyAttributeType.VALUE);
        string response = _commandSender.SendCommand(command);
        return _responseParser.Parse<string>(response);
    }

    public IResponse<string> GetHumidity()
    {
        ICommand command = new GetCommand(DeviceProperty.HUMIDITY, PropertyAttributeType.VALUE);
        string response = _commandSender.SendCommand(command);
        return _responseParser.Parse<string>(response);
    }
    
    #endregion

    #region Grating
    public IResponse<string> GetGratingId()
    {
        ICommand command = new GetCommand(DeviceProperty.GRATING_ID, PropertyAttributeType.VALUE);
        string response = _commandSender.SendCommand(command);
        return _responseParser.Parse<string>(response);
    }

    public IResponse<string> SetGratingId(string gratingId)
    {
        ICommand command = new SetCommand<string>(DeviceProperty.GRATING_ID, PropertyAttributeType.VALUE, gratingId);
        string response = _commandSender.SendCommand(command);
        return _responseParser.Parse<string>(response);
    }

    public IResponse<float> GetGratingAngle()
    {
        ICommand command = new GetCommand(DeviceProperty.GRATING_ANGLE, PropertyAttributeType.VALUE);
        string response = _commandSender.SendCommand(command);
        return _responseParser.Parse<float>(response);
    }

    public IResponse<float> SetGratingAngle(float gratingAngle)
    {
        ICommand command = new SetCommand<float>(DeviceProperty.GRATING_ANGLE, PropertyAttributeType.VALUE, gratingAngle);
        string response = _commandSender.SendCommand(command);
        return _responseParser.Parse<float>(response);
    }

    public IResponse<float> GetGratingWaveLength()
    {
        ICommand command = new GetCommand(DeviceProperty.GRATING_WAVELENGTH, PropertyAttributeType.VALUE);
        string response = _commandSender.SendCommand(command);
        return _responseParser.Parse<float>(response);
    }

    public IResponse<float> SetGratingWaveLength(float gratingWaveLength)
    {
        ICommand command = new SetCommand<float>(DeviceProperty.GRATING_WAVELENGTH, PropertyAttributeType.VALUE, gratingWaveLength);
        string response = _commandSender.SendCommand(command);
        return _responseParser.Parse<float>(response);
    }

    public IResponse<float> GetGratingDensity()
    {
        ICommand command = new GetCommand(DeviceProperty.GRATING_DENSITY, PropertyAttributeType.VALUE);
        string response = _commandSender.SendCommand(command);
        return _responseParser.Parse<float>(response);
    }

    public IResponse<float> SetGratingDensity(float gratingDensity)
    {
        ICommand command = new SetCommand<float>(DeviceProperty.GRATING_DENSITY, PropertyAttributeType.VALUE, gratingDensity);
        string response = _commandSender.SendCommand(command);
        return _responseParser.Parse<float>(response);
    }

    #endregion

    #region Slit
    
    public IResponse<string> GetSlitId()
    {
        ICommand command = new GetCommand(DeviceProperty.SLIT_ID, PropertyAttributeType.VALUE);
        string response = _commandSender.SendCommand(command);
        return _responseParser.Parse<string>(response);
    }

    public IResponse<string> SetSlitId(string slitId)
    {
        ICommand command = new SetCommand<string>(DeviceProperty.SLIT_ID, PropertyAttributeType.VALUE, slitId);
        string response = _commandSender.SendCommand(command);
        return _responseParser.Parse<string>(response);
    }

    public IResponse<float> GetSlitWidth()
    {
        ICommand command = new GetCommand(DeviceProperty.SLIT_WIDTH, PropertyAttributeType.VALUE);
        string response = _commandSender.SendCommand(command);
        return _responseParser.Parse<float>(response);
    }

    public IResponse<float> SetSlitWidth(float slitWidth)
    {
        ICommand command = new SetCommand<float>(DeviceProperty.SLIT_WIDTH, PropertyAttributeType.VALUE, slitWidth);
        string response = _commandSender.SendCommand(command);
        return _responseParser.Parse<float>(response);
    }

    public IResponse<float> GetSlitAngle()
    {
        ICommand command = new GetCommand(DeviceProperty.SLIT_ANGLE, PropertyAttributeType.VALUE);
        string response = _commandSender.SendCommand(command);
        return _responseParser.Parse<float>(response);
    }

    public IResponse<float> SetSlitAngle(float slitAngle)
    {
        ICommand command = new SetCommand<float>(DeviceProperty.SLIT_ANGLE, PropertyAttributeType.VALUE, slitAngle);
        string response = _commandSender.SendCommand(command);
        return _responseParser.Parse<float>(response);
    }
    
    #endregion

    #region Focus

    public IResponse<float> GetFocusPosition()
    {
        ICommand command = new GetCommand(DeviceProperty.FOCUS_POSITION, PropertyAttributeType.VALUE);
        string response = _commandSender.SendCommand(command);
        return _responseParser.Parse<float>(response);
    }

    public IResponse<float> SetFocusPosition(float focusPosition)
    {
        ICommand command = new SetCommand<float>(DeviceProperty.FOCUS_POSITION, PropertyAttributeType.VALUE, focusPosition);
        string response = _commandSender.SendCommand(command);
        return _responseParser.Parse<float>(response);
    }
    
    #endregion

    #region LightSource

    public IResponse<string> GetLightSource()
    {
        ICommand command = new GetCommand(DeviceProperty.LIGHT_SOURCE, PropertyAttributeType.VALUE);
        string response = _commandSender.SendCommand(command);
        return _responseParser.Parse<string>(response);
    }

    public IResponse<string> SetLightSource(string lightSource)
    {
        ICommand command = new SetCommand<string>(DeviceProperty.LIGHT_SOURCE, PropertyAttributeType.VALUE, lightSource);
        string response = _commandSender.SendCommand(command);
        return _responseParser.Parse<string>(response);
    }
    
    #endregion
}