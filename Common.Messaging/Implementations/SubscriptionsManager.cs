using Common.Messaging.Abstractions;
using Common.Messaging.Events;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Common.Messaging.Implementations
{
    public partial class SubscriptionsManager : ISubscriptionsManager
    {
        private readonly Dictionary<string, List<SubscriptionInfo>> _handlers;
        private readonly List<Type> _eventTypes;

        public event EventHandler<string> OnEventRemoved;

        public SubscriptionsManager()
        {
            _handlers = new Dictionary<string, List<SubscriptionInfo>>();
            _eventTypes = new List<Type>();
        }

        public bool IsEmpty => !_handlers.Keys.Any();

        public void Clear() => _handlers.Clear();

        public string GetEventKey<TKey>() => typeof(TKey).Name;

        public bool HasSubscriptions<TEvent>() where TEvent : Event
            => _handlers.ContainsKey(typeof(TEvent).Name);

        public void AddSubscription<TEvent, TEventHandler>()
            where TEvent : Event
            where TEventHandler : IEventHandler<TEvent>
        {
            var eventName = GetEventKey<TEvent>();
            var eventHandlerTypeToBeAdded = typeof(TEventHandler);

            if (!_eventTypes.Contains(typeof(TEvent)))
            {
                _eventTypes.Add(typeof(TEvent));
            }

            if (!HasSubscriptions<TEvent>())
                _handlers.Add(eventName, new List<SubscriptionInfo>());

            if (_handlers[eventName].Any(eventHandler => eventHandler.GetType() == eventHandlerTypeToBeAdded))
                throw new ArgumentException(
                    $"Handler Type {eventHandlerTypeToBeAdded.Name} already registered for '{eventName}'", nameof(eventHandlerTypeToBeAdded));

            _handlers[eventName].Add(new SubscriptionInfo(eventHandlerTypeToBeAdded));
        }
        public void RemoveSubscription<TEvent, TEventHandler>()
            where TEvent : Event
            where TEventHandler : IEventHandler<TEvent>
        {
            var eventName = GetEventKey<TEvent>();
            var handlerToRemoveType = typeof(TEventHandler);
            var handlerToRemove = _handlers[eventName].SingleOrDefault(eventHandler => eventHandler.GetType() == handlerToRemoveType);

            if (handlerToRemove != null)
            {
                _handlers[eventName].Remove(handlerToRemove);
                if (!_handlers[eventName].Any())
                {
                    _handlers.Remove(eventName);
                    var eventType = _eventTypes.SingleOrDefault(e => e.Name == eventName);
                    if (eventType != null)
                    {
                        _eventTypes.Remove(eventType);
                    }

                    OnEventRemoved.Invoke(this, eventName);
                }
            }
        }

        public bool HasSubscriptions(string eventName)
        {
            return _handlers.ContainsKey(eventName);
        }

        public IEnumerable<SubscriptionInfo> GetHandlersForEvent<TEvent>() where TEvent : Event
        {
            var eventKey = GetEventKey<TEvent>();
            return GetHandlersForEvent(eventKey);
        }

        public IEnumerable<SubscriptionInfo> GetHandlersForEvent(string eventName)
        {
            return _handlers[eventName];
        }

        public Type GetEventTypeByName(string eventName)
        {
            return _eventTypes.SingleOrDefault(t => t.Name == eventName);
        }
    }
}
