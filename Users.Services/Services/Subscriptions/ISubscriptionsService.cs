using System;
using System.Linq;
using System.Threading.Tasks;
using Users.Core.Domain;

namespace Users.Services.Services.Subscriptions
{
    public interface ISubscriptionsService : IBaseService
    {
        Task<User> SendRequestToBeFriendAsync(Guid currentUserId, Guid targetUserId);
        Task RejectSubscription(Guid currentUserId, Guid targetUserId);
        IQueryable<UserSubscriber> GetUserSubscribers(Guid userId);
    }
}
