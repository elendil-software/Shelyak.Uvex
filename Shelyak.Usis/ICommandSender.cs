using Shelyak.Usis.Commands;

namespace Shelyak.Usis;

public interface ICommandSender
{
    string SendCommand(ICommand command);
}