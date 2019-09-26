using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Users.Services.Users.Models.Responses;

namespace Users.Services.Services.Friends
{
    //TODO Move models from users service
    public interface IFriendsService: IBaseService
    {
        Task<UserResponseModel> AddToFriendAsync(Guid currentUserId, Guid subscriberId);
        Task<UserResponseModel> RemoveFriendAsync(Guid currentUserId, Guid friendId);
        IEnumerable<BaseUserResponseModel> GetUserFriends(Guid userId);
    }
}
