using Shelyak.Usis.Enums;

namespace Shelyak.Usis.Commands
{
    public interface ICommandFactory
    {
        ICommand CreateCommand(CommandType commandType, DeviceProperty deviceProperty, PropertyAttributeType attributeType);
    }
}