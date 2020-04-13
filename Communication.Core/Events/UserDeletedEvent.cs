using Common.Messaging.Events;
using System;

namespace Communication.Core.Events
{
    public class UserDeletedEvent: Event
    {
        public Guid AccountId { get; set; }
    }
}
