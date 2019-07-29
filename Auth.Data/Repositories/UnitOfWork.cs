using Auth.Data.Entities;
using Auth.Data.Interfaces;
using Common.Core.Interfaces;
using System;
using System.Threading.Tasks;

namespace Auth.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AuthContext _dbContext;
        public IRepository<Account> AccountsRepository { get; }

        public UnitOfWork(AuthContext dbContext, IRepository<Account> accountsRepository)
        {
            _dbContext = dbContext;
            AccountsRepository = accountsRepository;
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
