using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Nestor.Commands;
using Nestor.Messages;


const string TOKEN = "";

var builder = Host.CreateApplicationBuilder(args);

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
    new Bot(TOKEN,
        provider.GetRequiredService<DiscordSocketClient>(),
        provider.GetRequiredService<ILogger<Bot>>(),
        provider.GetRequiredService<IMessagesProcessor>()));

var app = builder.Build();

var bot = app.Services.GetRequiredService<Bot>();
await bot.StartAsync();