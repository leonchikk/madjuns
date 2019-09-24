using Common.Core.Events;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Users.Services.Users.Models.Requests;
using Users.Services.Users.Models.Responses;

namespace Users.Services.Users.Interfaces
{
    public interface IUsersService : IBaseService
    {
        Task<UserResponseModel> AddToBlackListAsync(Guid currentUserId, Guid targetUserId);
        Task<UserResponseModel> AddToFriendAsync(Guid currentUserId, Guid subscriberId);
        Task<UserResponseModel> CreateUserAsync(UserCreatedEvent createdEvent);
        Task<UserResponseModel> RemoveFriendAsync(Guid currentUserId, Guid friendId);
        Task<UserResponseModel> UpdateUserAsync(Guid id, UpdateUserRequest request);
        Task<UserResponseModel> SendRequestToBeFriendAsync(Guid currentUserId, Guid targetUserId);
        Task RejectSubscription(Guid currentUserId, Guid targetUserId);
        Task RemoveFromBlackList(Guid currentUserId, Guid targetUserId);


        UserResponseModel GetUserById(Guid id);
        ProfileResponseModel GetUserProfile(Guid id);

        IEnumerable<BaseUserResponseModel> GetUsers();
        IEnumerable<BaseUserResponseModel> GetUserFriends(Guid userId);
        IEnumerable<BaseUserResponseModel> GetUserSubscribers(Guid userId);
        IEnumerable<BaseUserResponseModel> GetUserBlackList(Guid userId);
        IEnumerable<SettingResponseModel> GetUserSettings(Guid id);

        Task DeleteUserAsync(Guid id);
    }
}
