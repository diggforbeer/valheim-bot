// See https://aka.ms/new-console-template for more information
using da_bot;
using da_bot.Discord;
using da_bot.Steam;
using da_bot.Steam.Models;
using Microsoft.Extensions.Configuration;

var builder = new ConfigurationBuilder().AddJsonFile($"appsettings.json", true, true);
var config = builder.Build();

Console.WriteLine("Hello, World!");
DiscordService.PostMessage("Hello - Starting");

var allLines = File.ReadAllLines("example-data/valheim-server-stdout---supervisor-eakr4y_6.log");
foreach (var line in allLines)
{
    var logEventType = LogEventTypeDetector.Detect(line);

    var playerId = "";
    var player = new Player();

    switch (logEventType)
    {
        case LogEventType.Connected:
            Console.WriteLine($"{logEventType} - {line}");
            playerId = line.Split(" ").Last();
            player = await SteamService.GetPlayerInfo(playerId);
            DiscordService.PostMessage($"{player.Personaname} has connected");
            break;
        case LogEventType.Disconnected:
            Console.WriteLine($"{logEventType} - {line}");
            playerId = line.Split(" ").Last();
            player = await SteamService.GetPlayerInfo(playerId);
            DiscordService.PostMessage($"{player.Personaname} has disconnected");
            break;

    }

}






Console.WriteLine("Press any key to exit");
Console.ReadLine();



