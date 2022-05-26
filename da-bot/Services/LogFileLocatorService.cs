using da_bot.Discord;
using da_bot.Steam;
using da_bot.Steam.Models;
using Microsoft.Extensions.Configuration;

namespace da_bot.Services
{
    public class ValheimEventDetectorService
    {
        private DiscordService _discordService;
        private SteamService _steamService;
        private LogEventTypeDetector _logEventTypeDetector;

        public ValheimEventDetectorService(IConfigurationRoot config)
        {
            _discordService = new DiscordService(config["discordWebHook"]);
            _steamService = new SteamService(config["steamApiKey"]);
            _logEventTypeDetector = new LogEventTypeDetector();
        }

        public async void Start(FileInfo latestFile)
        {
            using (StreamReader reader = new StreamReader(new FileStream(latestFile.FullName,
                     FileMode.Open, FileAccess.Read, FileShare.ReadWrite)))
            {
                //start at the end of the file
                long lastMaxOffset = reader.BaseStream.Length;

                while (true)
                {
                    Thread.Sleep(500);

                    //if the file size has not changed, idle
                    if (reader.BaseStream.Length == lastMaxOffset)
                        continue;

                    //seek to the last max offset
                    reader.BaseStream.Seek(lastMaxOffset, SeekOrigin.Begin);

                    //read out of the file until the EOF
                    string? line = "";
                    while ((line = reader.ReadLine()) != null)
                    {
                        var logEventType = _logEventTypeDetector.Detect(line);
                        var player = new Player();

                        string? playerId;
                        switch (logEventType)
                        {
                            case LogEventType.Connected:
                                Console.WriteLine($"{logEventType} - {line}");
                                playerId = line.Split(" ").Last();
                                player = await _steamService.GetPlayerInfo(playerId);
                                _discordService.PostMessage(text: $"{player?.Personaname} has connected");
                                break;
                            case LogEventType.Disconnected:
                                Console.WriteLine($"{logEventType} - {line}");
                                playerId = line.Split(" ").Last();
                                player = await _steamService.GetPlayerInfo(playerId);
                                _discordService.PostMessage(text: $"{player?.Personaname} has disconnected");
                                break;
                            case LogEventType.Death:
                                Console.WriteLine($"{logEventType} - {line}");
                                var deathPlayerName = line.Split(":").First().Split(" ").Last();
                                _discordService.PostMessage(text: $"{deathPlayerName} has died");
                                break;
                            case LogEventType.ServerStarted:
                                Console.WriteLine($"{logEventType} - {line}");
                                _discordService.PostMessage(text: $"Server has started");
                                break;

                        }
                    }

                    //update the last max offset
                    lastMaxOffset = reader.BaseStream.Position;
                }
            }
        }
    }
}
