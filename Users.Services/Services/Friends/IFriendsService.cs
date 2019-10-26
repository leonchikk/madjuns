using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Core.Domain;

namespace Users.Services.Services.Friends
{
    public interface IFriendsService: IBaseService
    {
        Task<User> AddToFriendAsync(Guid currentUserId, Guid subscriberId);
        Task<User> RemoveFriendAsync(Guid currentUserId, Guid friendId);
        IQueryable<UserFriend> GetUserFriends(Guid userId);
    }
}
