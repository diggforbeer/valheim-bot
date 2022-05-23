using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace da_bot
{
    public static class LogEventTypeDetector
    {
        public static LogEventType Detect(string logText)
        {
            var eventType = LogEventType.Unknown;


            if (logText.Contains("Got connection SteamID"))
            {
                return LogEventType.Connected;
            }
            else if (logText.Contains("Closing socket"))
            {
                return LogEventType.Disconnected;
            }



            return eventType;
        }
    }
}
