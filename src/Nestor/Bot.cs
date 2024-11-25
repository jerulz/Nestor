using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Logging;
using Nestor.Commands;
using Nestor.Messages;

public class Bot
{
    private readonly DiscordSocketClient _client;
    private readonly string? _token;
    private readonly ILogger<Bot> _logger;
    private readonly IMessagesProcessor _messagesProcessor;
 

    public Bot(string? token, DiscordSocketClient client, ILogger<Bot> logger, IMessagesProcessor messagesProcessor)
    {
        _client = client;
        _token = token;
        _logger = logger;
        _messagesProcessor = messagesProcessor;
    }

    public async Task StartAsync()
    {
        
        _client.Log += LogAsync;
        _client.Ready += ReadyAsync;
        _client.MessageReceived += MessageReceivedAsync;

        await _client.LoginAsync(TokenType.Bot, _token);
        await _client.StartAsync();

        await Task.Delay(-1); // Keep the bot running
    }

    private Task LogAsync(LogMessage log)
    {
        Console.WriteLine(log.ToString());
        return Task.CompletedTask;
    }

    private Task ReadyAsync()
    {
        Console.WriteLine("Bot is connected!");
        return Task.CompletedTask;
    }


    private async Task MessageReceivedAsync(SocketMessage message)
    {
        if (message is not SocketUserMessage userMessage)
        {
            _logger.LogInformation("Ignoring non-user message");
            return;
        }

        await _messagesProcessor.ProcessMessageAsync(userMessage);
    }
}