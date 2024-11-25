using Discord.WebSocket;

namespace Nestor.Messages;

public interface IMessagesProcessor
{
    Task ProcessMessageAsync(SocketUserMessage message);
}