using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Users.API.Models.Requests;
using Users.API.Models.Responses;

namespace Users.API.Interfaces
{
    public interface IUsersService
    {
        Task<IEnumerable<UserResponseModel>> GetUsers();
        Task<UserResponseModel> GetUserByIdAsync(Guid Id);
        Task<ProfileResponseModel> GetUserProfileAsync(Guid Id);
        Task<ProfileResponseModel> GetUserSettingsAsync(Guid Id);
        Task<UserResponseModel> UpdateUserAsync(UpdateUserRequest request);
        Task DeleteUserAsync(Guid Id);
    }
}
