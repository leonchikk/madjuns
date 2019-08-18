namespace Common.Core.Events
{
    public class SendMailEvent
    {
        public string Subject { get; set; }
        public string To { get; set; }
        public string Body { get; set; }
    }
}
