
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

        public UnitOfWork(UsersContext dbContext, IRepository<User> usersRepository)
        {
            _dbContext = dbContext;
            UsersRepository = usersRepository;
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
