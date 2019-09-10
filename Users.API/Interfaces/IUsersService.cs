using Common.Core.Events;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Users.API.Models.Requests;
using Users.API.Models.Responses;

namespace Users.API.Interfaces
{
    public interface IUsersService : IBaseService
    {
        Task<UserResponseModel> AddToBlackList(Guid currentUserId, Guid targetUserId);
        Task<UserResponseModel> AddToFriend(Guid currentUserId, Guid subscriberId);
        Task<UserResponseModel> CreateUserAsync(UserCreatedEvent createdEvent);
        Task<UserResponseModel> RemoveFriend(Guid currentUserId, Guid friendId);
        Task<UserResponseModel> UpdateUserAsync(Guid id, UpdateUserRequest request);
        Task<UserResponseModel> SendRequestToBeFriend(Guid currentUserId, Guid targetUserId);

        UserResponseModel GetUserById(Guid id);
        ProfileResponseModel GetUserProfile(Guid id);

        IEnumerable<UserResponseModel> GetUsers();
        IEnumerable<UserResponseModel> GetUserFriends(Guid userId);
        IEnumerable<UserResponseModel> GetUserSubscribers(Guid userId);
        IEnumerable<UserResponseModel> GetUserBlackList(Guid userId);
        IEnumerable<SettingResponseModel> GetUserSettings(Guid id);

        Task DeleteUserAsync(Guid id);
    }
}
