using Should;

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
            Result.ShouldEqual(da_bot.LogEventType.ServerStarted);
        }

        [TestMethod]
        public void ShouldNotBeUnknown()
        {
            Result.ShouldNotEqual(da_bot.LogEventType.Unknown);
        }
    }
}
