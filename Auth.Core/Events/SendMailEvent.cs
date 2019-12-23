using Common.Messaging.Events;

namespace Auth.Core.Events
{
    public class SendMailEvent : Event
    {
        public string Subject { get; set; }
        public string To { get; set; }
        public string Body { get; set; }
    }
}
