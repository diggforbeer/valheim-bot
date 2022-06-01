namespace da_bot_unitTests.EventTests.DeathEventTests
{
    [TestClass]
    public class ShouldHandleNatedogDeath : DeathContext
    {

        public ShouldHandleNatedogDeath()
        {
            Command.Handle("Got character ZDOID from Natedog : 0:0");

        }

        [TestMethod]
        public void ShouldCallDiscord()
        {
            MockDiscordService.Verify(x => x.PostMessage(Moq.It.IsAny<string>()), Moq.Times.Exactly(1));
        }
    }
}