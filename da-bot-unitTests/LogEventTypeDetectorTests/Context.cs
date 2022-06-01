using da_bot;
using da_bot.Discord;
using da_bot.Steam;
using Moq;

namespace da_bot_unitTests.LogEventTypeDetectorTests
{
    [TestClass]
    public class Context
    {
        public LogEventTypeDetector Command;
        public EventTypeMapping? Result;

        public Context()
        {

            var discordService = new Mock<IDiscordService>();
            var steamService = new Mock<ISteamService>();

            Command = new LogEventTypeDetector(discordService.Object, steamService.Object);
        }
    }
}