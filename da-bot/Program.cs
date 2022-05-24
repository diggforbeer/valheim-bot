// See https://aka.ms/new-console-template for more information
using da_bot;
using da_bot.Discord;
using da_bot.Steam;
using da_bot.Steam.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections;


var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

var builder = new ConfigurationBuilder().AddJsonFile($"appsettings.json", true, true)
            .AddJsonFile($"appsettings.{environmentName}.json", true, true)
            .AddEnvironmentVariables();

var config = builder.Build();

Console.WriteLine("GetEnvironmentVariables: ");
foreach (DictionaryEntry de in Environment.GetEnvironmentVariables())
    Console.WriteLine("  {0} = {1}", de.Key, de.Value);

Console.WriteLine("Hello, World!");
var discordService = new DiscordService(config["discordWebHook"]);
var steamService = new SteamService(config["steamApiKey"]);
var eventDetector = new LogEventTypeDetector();

var directory = new DirectoryInfo(config["logFolder"]);

var latestFile = directory.GetFiles("valheim-server-stdout*.log").OrderByDescending(x => x.LastWriteTime).First();

using (StreamReader reader = new StreamReader(new FileStream(latestFile.FullName,
                     FileMode.Open, FileAccess.Read, FileShare.ReadWrite)))
{
    //start at the end of the file
    long lastMaxOffset = reader.BaseStream.Length;

    while (true)
    {
        System.Threading.Thread.Sleep(100);

        //if the file size has not changed, idle
        if (reader.BaseStream.Length == lastMaxOffset)
            continue;

        //seek to the last max offset
        reader.BaseStream.Seek(lastMaxOffset, SeekOrigin.Begin);

        //read out of the file until the EOF
        string line = "";
        while ((line = reader.ReadLine()) != null)
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
            

        //update the last max offset
        lastMaxOffset = reader.BaseStream.Position;
    }
}

Console.WriteLine("Press any key to exit");
Console.ReadLine();
