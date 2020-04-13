using Common.Messaging.Events;
using System;

namespace Communication.Core.Events
{
    public class UserCreatedEvent : Event
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
    }
}
