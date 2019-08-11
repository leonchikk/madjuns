using Common.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Users.Core.Domain;

namespace Users.Core.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<User> UsersRepository { get; }
        IRepository<Profile> ProfilesRepository { get; }
        IRepository<Setting> SettingsRepository { get; }
        IRepository<UserSetting> UserSettingsRepository { get; }

        Task SaveAsync();
    }
}
