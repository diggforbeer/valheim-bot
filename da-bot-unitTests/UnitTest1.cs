using da_bot;

namespace da_bot_unitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var login = LogEventTypeDetector.Detect("05/18/2022 16:35:38: Got connection SteamID 76561197975931951");

            Assert.AreEqual(login, LogEventType.Connected);
        }
    }
}