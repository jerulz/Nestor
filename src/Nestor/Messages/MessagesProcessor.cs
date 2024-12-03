using Discord.WebSocket;
using Microsoft.Extensions.Logging;
using Nestor.Commands;

namespace Nestor.Messages;

public class MessagesProcessor(ILogger<MessagesProcessor> logger, ICommandsProcessor commandsProcessor) : IMessagesProcessor
{
    public async Task ProcessMessageAsync(SocketUserMessage message)
    {
        logger.LogInformation("Received message from {AuthorUsername}: {MessageContent}", message.Author.Username, message.Content);

        if (string.IsNullOrEmpty(message.Content))
        {
            logger.LogInformation("Ignoring empty message");
        }

        if (message.Content.StartsWith("!"))
        {
            await commandsProcessor.ProcessCommandAsync(message);
        }
    }
}