using Common.Core.Models;

namespace Users.Core.Domain
{
    public class UserSubscriber : BaseEntity
    {
        public User User { get; set; }
        public User Subscriber { get; set; }
    }
}
