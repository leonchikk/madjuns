using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Messaging.Events
{
    public class Event
    {
        public Guid Id { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
