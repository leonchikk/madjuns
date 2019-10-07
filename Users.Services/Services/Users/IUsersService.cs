using Common.Core.Events;
using System;
using System.Linq;
using System.Threading.Tasks;
using Users.Core.Domain;
using Users.Services.Models.Requests;

namespace Users.Services.Users.Interfaces
{
    public interface IUsersService : IBaseService
    {
        Task<User> CreateUserAsync(UserCreatedEvent createdEvent);
        Task<User> UpdateUserAsync(Guid id, UpdateUserRequest request);

        User GetUserById(Guid id);
        Profile GetUserProfile(Guid id);

        IQueryable<User> GetUsers();
        IQueryable<UserSetting> GetUserSettings(Guid id);

        Task DeleteUserAsync(Guid id);
    }
}
