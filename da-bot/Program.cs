// See https://aka.ms/new-console-template for more information
using da_bot;
using da_bot.Discord;
using da_bot.Steam;
using da_bot.Steam.Models;
using Microsoft.Extensions.Configuration;


var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

var builder = new ConfigurationBuilder().AddJsonFile($"appsettings.json", true, true)
            .AddJsonFile($"appsettings.{environmentName}.json", true, true)
            .AddEnvironmentVariables();

var config = builder.Build();

Console.WriteLine("Hello, World!");
var discordService = new DiscordService(config["discordWebHook"]);
var steamService = new SteamService(config["steamApiKey"]);
var eventDetector = new LogEventTypeDetector();

discordService.PostMessage("Hello - Starting");

var allLines = File.ReadAllLines("example-data/valheim-server-stdout---supervisor-eakr4y_6.log");
foreach (var line in allLines)
{
    var logEventType = eventDetector.Detect(line);

    var playerId = "";
    var player = new Player();

    switch (logEventType)
    {
        case LogEventType.Connected:
            Console.WriteLine($"{logEventType} - {line}");
            playerId = line.Split(" ").Last();
            player = await steamService.GetPlayerInfo(playerId);
            discordService.PostMessage(text: $"{player?.Personaname} has connected");
            break;
        case LogEventType.Disconnected:
            Console.WriteLine($"{logEventType} - {line}");
            playerId = line.Split(" ").Last();
            player = await steamService.GetPlayerInfo(playerId);
            discordService.PostMessage(text: $"{player?.Personaname} has disconnected");
            break;

    }

}






Console.WriteLine("Press any key to exit");
Console.ReadLine();



