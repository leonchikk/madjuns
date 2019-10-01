using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Users.Services.Models.Responses;

namespace Users.Services.Services.Friends
{
    public interface IFriendsService: IBaseService
    {
        Task<UserResponseModel> AddToFriendAsync(Guid currentUserId, Guid subscriberId);
        Task<UserResponseModel> RemoveFriendAsync(Guid currentUserId, Guid friendId);
        IEnumerable<BaseUserResponseModel> GetUserFriends(Guid userId);
    }
}
