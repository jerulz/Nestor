using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Nestor.Commands;
using Nestor.Messages;

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

var builder = Host.CreateApplicationBuilder(args);

string? token = configuration["DiscordToken"];
builder.Services.AddSingleton<ILoggerFactory, LoggerFactory>();
builder.Services.AddSingleton(typeof(ILogger<>), typeof(Logger<>));
builder.Services.AddSingleton<ICommandsProcessor, CommandsProcessor>();
builder.Services.AddSingleton<IMessagesProcessor, MessagesProcessor>();
builder.Services.AddSingleton<DiscordSocketClient>(provider =>
    new DiscordSocketClient(new DiscordSocketConfig
    {
        GatewayIntents = GatewayIntents.AllUnprivileged | GatewayIntents.MessageContent
    }));
builder.Services.AddSingleton<Bot>(provider =>
    new Bot(token,
        provider.GetRequiredService<DiscordSocketClient>(),
        provider.GetRequiredService<ILogger<Bot>>(),
        provider.GetRequiredService<IMessagesProcessor>()));

var app = builder.Build();

var bot = app.Services.GetRequiredService<Bot>();
await bot.StartAsync();