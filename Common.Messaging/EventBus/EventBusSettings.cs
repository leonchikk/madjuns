using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Messaging.EventBus
{
    public class EventBusSettings
    {
        public string HostName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ClientName { get; set; }
    }
}
