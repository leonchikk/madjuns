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
        UserResponseModel GetUserById(Guid Id);
        ProfileResponseModel GetUserProfile(Guid Id);
        IEnumerable<SettingResponseModel> GetUserSettings(Guid Id);
        Task<UserResponseModel> UpdateUserAsync(Guid id, UpdateUserRequest request);
        Task DeleteUserAsync(Guid Id);
    }
}
