using da_bot.Discord;
using da_bot.Events;
using da_bot.Steam;
using Microsoft.Extensions.Configuration;

namespace da_bot
{
    public class LogEventTypeDetector
    {
        private readonly IDiscordService? _discordService;
        private readonly ISteamService? _steamService;
        private List<EventTypeMapping> eventTypeMappings;

        public LogEventTypeDetector(IDiscordService discordService, ISteamService steamService)
        {
            _discordService = discordService;
            _steamService = steamService;
            eventTypeMappings = new()
        {
            new EventTypeMapping(){LogEventType = LogEventType.Connected, LogMessage ="Got connection SteamID", EventClass = new ConnectedEvent(_discordService,_steamService)},
            new EventTypeMapping(){LogEventType = LogEventType.Disconnected, LogMessage ="Closing socket",EventClass = new DisconnectedEvent(_discordService,_steamService)},
            new EventTypeMapping(){LogEventType = LogEventType.Death, LogMessage = " : 0:0",EventClass = new DeathEvent(_discordService,_steamService)}, //Got character ZDOID from Diggforbeer : 0:0
            new EventTypeMapping(){LogEventType = LogEventType.ServerStarted, LogMessage = "Game server connected",EventClass = new ServerStartedEvent(_discordService,_steamService)},
        };

        }



        public EventTypeMapping? Detect(string logText)
        {

            var detectedEventType = eventTypeMappings.SingleOrDefault(x => logText.Contains(x.LogMessage));
            if (detectedEventType == null)
                return null;
            else
                return detectedEventType;
        }
    }
}
