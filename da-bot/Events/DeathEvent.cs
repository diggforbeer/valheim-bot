using da_bot.Discord;
using da_bot.Steam;

namespace da_bot.Events
{
    internal class DeathEvent : BaseEvent
    {
        public DeathEvent(IDiscordService discordService, ISteamService steamService) : base(discordService, steamService)
        {
        }

        public override LogEventType LogEventType { get => LogEventType.Death; }

        public async override void Handle(string log)
        {
            var deathPlayerName = line.Split(":").First().Split(" ")[4];
            await _discordService.PostMessage(text: $"{deathPlayerName} has died");
        }
    }
}
