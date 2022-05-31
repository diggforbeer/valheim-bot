using da_bot.Events;

namespace da_bot
{
    public class EventTypeMapping
    {
        public string LogMessage { get; set; } = String.Empty;
        public LogEventType LogEventType { get; set; }

        public BaseEvent? EventClass { get; set; }
    }
}
