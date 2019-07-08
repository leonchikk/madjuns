using Accounts.Data.Entities;
using Common.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Accounts.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AccountsContext _dbContext;
        public readonly IRepository<Account> AccountsRepository;

        public UnitOfWork(AccountsContext dbContext, IRepository<Account> accountsRepository)
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
