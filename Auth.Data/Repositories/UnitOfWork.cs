using Auth.Core.Entities;
using Common.Core.Interfaces;
using System;
using System.Threading.Tasks;

namespace Auth.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AuthContext _dbContext;

        public UnitOfWork(AuthContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Dispose()
        {
            _dbContext.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
