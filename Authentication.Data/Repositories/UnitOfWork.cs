using Authentication.Data.Entities;
using Authentication.Data.Interfaces;
using Common.Core.Interfaces;
using System;
using System.Threading.Tasks;

namespace Authentication.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AuthenticationContext _dbContext;
        public IRepository<User> UsersRepository { get; }

        public UnitOfWork(AuthenticationContext dbContext, IRepository<User> usersRepository)
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
