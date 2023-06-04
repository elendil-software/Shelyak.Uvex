using Shelyak.Usis.Enums;

namespace Shelyak.Usis.Commands;

public class GetCommand : ICommand
{
    private readonly DeviceProperty _deviceProperty;
    private readonly PropertyAttributeType _attributeType;
    
    public GetCommand(DeviceProperty deviceProperty, PropertyAttributeType attributeType)
    {
        _deviceProperty = deviceProperty;
        _attributeType = attributeType;
    }
    
    public string Build()
    {
        return $"{CommandType.GET};{_deviceProperty};{_attributeType}";
    }
}