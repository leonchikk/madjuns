using Common.Core.Events;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Users.Services.Models.Requests;
using Users.Services.Models.Responses;

namespace Users.Services.Users.Interfaces
{
    public interface IUsersService : IBaseService
    {
        Task<UserResponseModel> CreateUserAsync(UserCreatedEvent createdEvent);
        Task<UserResponseModel> UpdateUserAsync(Guid id, UpdateUserRequest request);

        UserResponseModel GetUserById(Guid id);
        ProfileResponseModel GetUserProfile(Guid id);

        IEnumerable<BaseUserResponseModel> GetUsers();
        IEnumerable<SettingResponseModel> GetUserSettings(Guid id);

        Task DeleteUserAsync(Guid id);
    }
}
