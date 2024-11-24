using Discord;
using Discord.WebSocket;
using System;
using System.Threading.Tasks;

public class Bot
{
    private readonly DiscordSocketClient _client;
    private readonly string _token;

    public Bot(string token)
    {
        var config = new DiscordSocketConfig
        {
            GatewayIntents = GatewayIntents.AllUnprivileged | GatewayIntents.MessageContent
        };
        _client = new DiscordSocketClient(config);
        _token = token;
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
            Console.WriteLine("Ignoring non-user message");
            return;
        }

        Console.WriteLine($"Received message from {userMessage.Author.Username}: {userMessage.Content}");

        if (userMessage.Author.IsBot)
        {
            Console.WriteLine("Ignoring message from bot");
            return;
        }

        if (string.IsNullOrEmpty(userMessage.Content))
        {
            Console.WriteLine("Ignoring empty message");
            return;
        }

        if (userMessage.Content == "!ping")
        {
            Console.WriteLine("Received !ping command");
            await userMessage.Channel.SendMessageAsync("Pong!");
        }
        else
        {
            Console.WriteLine($"Message content: {userMessage.Content}");
        }
    }
}