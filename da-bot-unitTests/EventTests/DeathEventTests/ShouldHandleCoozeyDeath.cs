namespace da_bot_unitTests.EventTests.DeathEventTests
{
    [TestClass]
    public class ShouldHandleCoozeyDeath : DeathContext
    {

        public ShouldHandleCoozeyDeath()
        {
            Command.Handle("Got character ZDOID from Coozey : 0:0");
        }

        [TestMethod]
        public void ShouldCallDiscord()
        {
            MockDiscordService.Verify(x => x.PostMessage(Moq.It.IsAny<string>()), Moq.Times.Exactly(1));
        }
    }
}