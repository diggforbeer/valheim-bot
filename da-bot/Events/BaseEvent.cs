using da_bot.Discord;
using da_bot.Steam;

namespace da_bot.Events
{
    public abstract class BaseEvent
    {
        public IDiscordService _discordService;
        public ISteamService _steamService;

        public BaseEvent(IDiscordService discordService, ISteamService steamService)
        {
            _discordService = discordService;
            _steamService = steamService;
        }


        public abstract LogEventType LogEventType { get; }

        public abstract void Handle(string log);
    }
}
