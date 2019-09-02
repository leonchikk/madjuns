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
        Task<UserResponseModel> CreateUserAsync(UserCreatedEvent createdEvent);
        IEnumerable<UserResponseModel> GetUsers();
        UserResponseModel GetUserById(Guid id);
        ProfileResponseModel GetUserProfile(Guid id);
        IEnumerable<SettingResponseModel> GetUserSettings(Guid id);
        Task<UserResponseModel> UpdateUserAsync(Guid id, UpdateUserRequest request);
        Task<UserResponseModel> AddToFriend(Guid currentUserId, Guid subscriberId);
        Task<UserResponseModel> RemoveFriend(Guid currentUserId, Guid friendId);
        Task<UserResponseModel> AddToBlackList(Guid currentUserId, Guid targetUserId);
        Task<UserResponseModel> SendRequestToBeFriend(Guid currentUserId, Guid targetUserId);
        Task DeleteUserAsync(Guid id);
    }
}
