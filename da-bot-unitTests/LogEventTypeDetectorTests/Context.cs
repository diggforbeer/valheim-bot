using da_bot;

namespace da_bot_unitTests.LogEventTypeDetectorTests
{
    [TestClass]
    public class Context
    {
        public LogEventTypeDetector Command;
        public LogEventType Result;
        
        public Context()
        {
            Command = new LogEventTypeDetector();
        }
    }
}