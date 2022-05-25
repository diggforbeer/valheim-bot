using FluentAssertions;

namespace da_bot_unitTests.LogEventTypeDetectorTests
{
    [TestClass]
    public class ShouldReportDeath : Context
    {

        private string testString = "Got character ZDOID from Diggforbeer : 0:0";


        public ShouldReportDeath()
        {
            Result = Command.Detect(testString);
        }

        [TestMethod]
        public void ShouldBeDeath()
        {
            Result.Should().Be(da_bot.LogEventType.Death);
        }

        [TestMethod]
        public void ShouldNotBeUnknown()
        {
            Result.Should().NotBe(da_bot.LogEventType.Unknown);
        }
    }
}
