
using Common.Core.Interfaces;
using System;
using System.Threading.Tasks;
using Users.Core.Domain;
using Users.Core.Interfaces;

namespace Users.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly UsersContext _dbContext;
        public IRepository<User> UsersRepository { get; }
        public IRepository<Profile> ProfilesRepository { get; }
        public IRepository<Setting> SettingsRepository { get; }
        public IRepository<UserSetting> UserSettingsRepository { get; }

        public UnitOfWork
        (
            UsersContext dbContext, 
            IRepository<User> usersRepository,
            IRepository<Profile> profilesRepository,
            IRepository<Setting> settingsRepository,
            IRepository<UserSetting> userSettingsRepository
        )
        {
            _dbContext = dbContext;
            UsersRepository = usersRepository;
            ProfilesRepository = profilesRepository;
            SettingsRepository = settingsRepository;
            UserSettingsRepository = userSettingsRepository;
        }

        public void Dispose()
        {
            _dbContext.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
