using Shelyak.Usis.Enums;

namespace Shelyak.Usis.Commands
{
    public interface ICommandFactory
    {
        ICommand CreateCommand<T>(CommandType commandType, DeviceProperty deviceProperty, PropertyAttributeType attributeType);
        
        ICommand CreateCommand<T>(CommandType commandType, DeviceProperty deviceProperty, PropertyAttributeType attributeType, T value);
    }
}