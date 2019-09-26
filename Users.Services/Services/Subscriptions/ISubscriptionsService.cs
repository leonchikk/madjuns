using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Users.Services.Users.Models.Responses;

namespace Users.Services.Services.Subscriptions
{
    //TODO Move models from users service
    public interface ISubscriptionsService: IBaseService
    {
        Task<UserResponseModel> SendRequestToBeFriendAsync(Guid currentUserId, Guid targetUserId);
        Task RejectSubscription(Guid currentUserId, Guid targetUserId);
        IEnumerable<BaseUserResponseModel> GetUserSubscribers(Guid userId);
    }
}
