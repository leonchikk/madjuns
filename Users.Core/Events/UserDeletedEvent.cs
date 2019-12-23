using Common.Messaging.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Users.Core.Events
{
    public class UserDeletedEvent: Event
    {
        public Guid AccountId { get; set; }
    }
}
