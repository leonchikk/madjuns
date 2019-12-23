using Common.Messaging.Abstractions;
using Notifications.Email.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Notifications.Email.EventHandlers
{
    public class ReceivedMailEventHandler : IEventHandler<ReceivedMailEvent>
    {
        public async Task HandleAsync(ReceivedMailEvent @event)
        {
            throw new NotImplementedException();
        }
    }
}
