using Common.Core.Models;
using System;

namespace Users.Core.Domain
{
    public class UserSubscriber : BaseEntity
    {
        protected UserSubscriber() { }
        public UserSubscriber(User target, User subscriber)
        {
            Id = Guid.NewGuid();
            User = target;
            Subscriber = subscriber;
        }

        public virtual Guid UserId { get; set; }
        public virtual Guid SubscriberId { get; set; }

        public virtual User User { get; set; }
        public virtual User Subscriber { get; set; }
    }
}
