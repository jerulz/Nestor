using System.Globalization;
using Discord.WebSocket;
using Microsoft.Extensions.Logging;
using Nestor.Messages;

namespace Nestor.Commands;

public class CommandsProcessor(ILogger<CommandsProcessor> logger) : ICommandsProcessor
{
    public async Task ProcessCommandAsync(SocketUserMessage message)
    {
        if (message.Content == "!ping")
        {
            await message.Channel.SendMessageAsync("Pong!");
        }
        if (message.Content == "!time")
        {
            await message.Channel.SendMessageAsync(DateTime.Now.ToString(CultureInfo.InvariantCulture).Split(' ')[1]);
        }
        if(message.Content == "!stats")
        {
            string userName = message.Author.Username;
            await message.Channel.SendMessageAsync("[STATS] "+ userName +" =>  Not Implemented yet");
        }
        if(message.Content == "!help")
        {
            await message.Channel.SendMessageAsync("Commands: !ping, !time, !stats, !help");
        }
    }
}