using System;
using System.Linq;
using System.Threading.Tasks;
using Users.Core.Domain;
using Users.Core.Events;

namespace Users.Services.Users.Interfaces
{
    public interface IUsersService : IBaseService
    {
        Task<User> CreateUserAsync(UserCreatedEvent createdEvent);
        Task<User> UpdateUserAsync(Guid id, Profile request);

        User GetUserById(Guid id);
        Profile GetUserProfile(Guid id);

        IQueryable<User> GetUsers();
        IQueryable<UserSetting> GetUserSettings(Guid id);

        Task DeleteUserAsync(Guid id);
    }
}
