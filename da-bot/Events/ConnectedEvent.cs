using da_bot.Discord;
using da_bot.Steam;

namespace da_bot.Events
{
    internal class ConnectedEvent : BaseEvent
    {
        public ConnectedEvent(IDiscordService discordService, ISteamService steamService) : base(discordService, steamService)
        {
        }

        public override LogEventType LogEventType { get => LogEventType.Connected; }

        public async override void Handle(string log)
        {
            var playerId = log.Split(" ").Last();
            var player = await _steamService.GetPlayerInfo(playerId);
            _discordService.PostMessage(text: $"{player?.Personaname} has connected");
        }
    }
}
