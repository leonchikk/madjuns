using Common.Messaging.Events;

namespace Notifications.Email.Events
{
    public class SendMailEvent : Event
    {
        public string To { get; set; }
        public string Body { get; set; }
        public string Subject { get; set; }
    }
}
