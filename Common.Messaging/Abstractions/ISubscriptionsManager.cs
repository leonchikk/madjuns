using Common.Messaging.EventBus;
using Common.Messaging.Events;
using System;
using System.Collections.Generic;
using static Common.Messaging.Implementations.SubscriptionsManager;

namespace Common.Messaging.Abstractions
{
    public interface ISubscriptionsManager
    {
        bool IsEmpty { get; }
        event EventHandler<string> OnEventRemoved;

        void AddSubscription<TEvent, TEventHandler>()
            where TEvent : Event
            where TEventHandler : IEventHandler<TEvent>;

        void RemoveSubscription<TEvent, TEventHandler>()
            where TEvent : Event
            where TEventHandler : IEventHandler<TEvent>;

        bool HasSubscriptions<TEvent>()
            where TEvent : Event;

        bool HasSubscriptions(string eventName);

        void Clear();

        string GetEventKey<TKey>();

        Type GetEventTypeByName(string eventName);

        IEnumerable<SubscriptionInfo> GetHandlersForEvent<TEvent>()
             where TEvent : Event;

        IEnumerable<SubscriptionInfo> GetHandlersForEvent(string eventName);
    }
}
