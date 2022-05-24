using FluentAssertions;

namespace da_bot_unitTests.LogEventTypeDetectorTests
{
    [TestClass]
    public class ShouldReportServerStarted : Context
    {

        private string testString = "Game server connected";


        public ShouldReportServerStarted()
        {
            Result = Command.Detect(testString);
        }

        [TestMethod]
        public void ShouldBeStarted()
        {
            Result.Should().Be(da_bot.LogEventType.ServerStarted);
        }

        [TestMethod]
        public void ShouldNotBeUnknown()
        {
            Result.Should().NotBe(da_bot.LogEventType.Unknown);
        }
    }
}
