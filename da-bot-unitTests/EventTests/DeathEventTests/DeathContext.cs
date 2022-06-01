using da_bot.Events;

namespace da_bot_unitTests.EventTests.DeathEventTests
{
    public class DeathContext : Context
    {
        public DeathEvent Command;
        public DeathContext()
        {
            Command = new DeathEvent(MockDiscordService.Object, MockSteamService.Object);
        }
    }
}
