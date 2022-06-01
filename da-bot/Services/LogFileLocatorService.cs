using da_bot.Discord;
using da_bot.Steam;
using da_bot.Steam.Models;
using Microsoft.Extensions.Configuration;

namespace da_bot.Services
{

    public interface IValheimEventDetectorService
    {
        void Start(FileInfo latestFile);
    }


    public class ValheimEventDetectorService: IValheimEventDetectorService
    {
        private LogEventTypeDetector _logEventTypeDetector;

        public ValheimEventDetectorService(IDiscordService discordService, ISteamService steamService)
        {
            _logEventTypeDetector = new LogEventTypeDetector(discordService, steamService);
        }

        public void Start(FileInfo latestFile)
        {
            using (StreamReader reader = new StreamReader(new FileStream(latestFile.FullName,
                     FileMode.Open, FileAccess.Read, FileShare.ReadWrite)))
             {
                //start at the end of the file
                long lastMaxOffset = reader.BaseStream.Length;

                while (true)
                {
                    Thread.Sleep(1000);
                    Console.WriteLine(".");

                    //if the file size has not changed, idle
                    if (reader.BaseStream.Length == lastMaxOffset)
                        continue;

                    //seek to the last max offset
                    reader.BaseStream.Seek(lastMaxOffset, SeekOrigin.Begin);

                    //read out of the file until the EOF
                    string? line = "";
                    while ((line = reader.ReadLine()) != null)
                    {
                        var logEventTypeMapping = _logEventTypeDetector.Detect(line);

                        if (logEventTypeMapping == null)
                            continue;

                        Console.WriteLine($"{logEventTypeMapping.LogEventType} - {line}");
                        logEventTypeMapping.EventClass?.Handle(line);
                    }

                    //update the last max offset
                    lastMaxOffset = reader.BaseStream.Position;
                }
            }
        }
    }
}
