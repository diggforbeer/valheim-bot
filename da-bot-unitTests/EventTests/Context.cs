using da_bot.Discord;
using da_bot.Steam;
using Moq;

namespace da_bot_unitTests.EventTests
{
    public class Context
    {

        public Mock<ISteamService> MockSteamService { get; set; }
        public Mock<IDiscordService> MockDiscordService { get; set; }

        public Context()
        {
            MockSteamService = new Mock<ISteamService>();
            MockDiscordService = new Mock<IDiscordService>();
        }
    }
}
