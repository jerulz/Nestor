using Discord.WebSocket;

namespace Nestor.Commands;

public interface ICommandsProcessor
{
    Task ProcessCommandAsync(SocketUserMessage cmd);
}