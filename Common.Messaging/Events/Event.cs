using System;

namespace Common.Messaging.Events
{
    public class Event
    {
        public Event()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
