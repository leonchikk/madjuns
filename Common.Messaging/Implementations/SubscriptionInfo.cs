using Common.Messaging.Abstractions;
using System;

namespace Common.Messaging.Implementations
{
    public partial class SubscriptionsManager : ISubscriptionsManager
    {
        public partial class SubscriptionInfo
        {
            public Type HandlerType { get; }

            public SubscriptionInfo(Type handlerType)
            {
                HandlerType = handlerType;
            }
        }
    }
}
