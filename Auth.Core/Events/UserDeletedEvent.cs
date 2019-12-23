using Common.Messaging.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Auth.Core.Events
{
    public class UserDeletedEvent: Event
    {
        public Guid AcountId { get; set; }
    }
}
