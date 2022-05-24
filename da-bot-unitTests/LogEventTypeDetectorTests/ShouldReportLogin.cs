using Should;

namespace da_bot_unitTests.LogEventTypeDetectorTests
{
    [TestClass]
    public class ShouldReportLogin : Context
    {

        private string testString = "05/18/2022 16:35:38: Got connection SteamID 76561197975931951";


        public ShouldReportLogin()
        {
            Result = Command.Detect(testString);
        }



        [TestMethod]
        public void ShouldBeConnected()
        {
            Result.ShouldEqual(da_bot.LogEventType.Connected);
        }

        [TestMethod]
        public void ShouldNotBeUnknown()
        {
            Result.ShouldNotEqual(da_bot.LogEventType.Unknown);
        }
    }
}
