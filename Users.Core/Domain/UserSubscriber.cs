using Common.Core.Models;

namespace Users.Core.Domain
{
    public class UserSubscriber : BaseEntity
    {
        private UserSubscriber() { }
        public UserSubscriber(User target, User subscriber)
        {
            User = target;
            Subscriber = subscriber;
        }

        public User User { get; set; }
        public User Subscriber { get; set; }
    }
}
