using Common.Messaging.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Auth.Core.Events
{
    public class UserCreatedEvent: Event
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
    }
}
