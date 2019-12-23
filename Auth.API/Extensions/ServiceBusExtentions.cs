using Common.Messaging.Abstractions;
using Common.Messaging.Events;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Auth.API.Extensions
{
    public static class ServiceBusExtentions
    {
        public static void SubscribeToEvent<TEvent, TEventHandler>(this IApplicationBuilder app) 
            where TEvent: Event
            where TEventHandler : IEventHandler<TEvent>
        {
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();

            eventBus.Subscribe<TEvent, TEventHandler>();
        }
    }
}
