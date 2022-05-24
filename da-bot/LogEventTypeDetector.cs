namespace da_bot
{
    public class LogEventTypeDetector
    {
        private List<EventTypeMapping> eventTypeMappings = new()
        {
            new EventTypeMapping(){LogEventType = LogEventType.Connected, LogMessage ="Got connection SteamID"},
            new EventTypeMapping(){LogEventType = LogEventType.Disconnected, LogMessage ="Closing socket"},
            new EventTypeMapping(){LogEventType = LogEventType.Death, LogMessage = " : 0:0"}, //Got character ZDOID from Diggforbeer : 0:0
            new EventTypeMapping(){LogEventType = LogEventType.ServerStarted, LogMessage = "Game server connected"},
        };

        public LogEventType Detect(string logText)
        {

            var detectedEventType = eventTypeMappings.SingleOrDefault(x => logText.Contains(x.LogMessage));
            if (detectedEventType == null)
                return LogEventType.Unknown;
            else
                return detectedEventType.LogEventType;
        }
    }
}
