using da_bot.Discord;
using da_bot.Steam;

namespace da_bot.Events
{
    internal class ServerStartedEvent : BaseEvent
    {
        public ServerStartedEvent(IDiscordService discordService, ISteamService steamService) : base(discordService, steamService)
        {
        }

        public override LogEventType LogEventType { get => LogEventType.ServerStarted; }

        public async override void Handle(string log)
        {
            await _discordService.PostMessage(text: $"Server has started");
        }
    }
}
