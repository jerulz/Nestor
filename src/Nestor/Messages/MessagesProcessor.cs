using System.Globalization;
using Discord.WebSocket;
using Microsoft.Extensions.Logging;
using Nestor.Commands;

namespace Nestor.Messages;

public class MessagesProcessor(ILogger<MessagesProcessor> logger, ICommandsProcessor commandsProcessor) : IMessagesProcessor
{
    public async Task ProcessMessageAsync(SocketUserMessage message)
    {
        logger.LogInformation("Received message from {AuthorUsername}: {MessageContent}", message.Author.Username, message.Content);

        if (message.Author.IsBot)
        {
            logger.LogInformation("Ignoring message from bot");
        }

        if (string.IsNullOrEmpty(message.Content))
        {
            logger.LogInformation("Ignoring empty message");
        }

        if (message.Content == "!ping")
        {
            logger.LogInformation("Received !ping command");
            await message.Channel.SendMessageAsync("Pong!");
        }

        if (message.Content == "!time")
        {
            logger.LogInformation("Received !time command");
            await message.Channel.SendMessageAsync(
                DateTime.Now.ToString(CultureInfo.InvariantCulture).Split(' ')[1]);
        }
    }
}