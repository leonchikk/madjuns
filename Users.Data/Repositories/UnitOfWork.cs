
using Common.Core.Interfaces;
using System;
using System.Threading.Tasks;

namespace Users.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly UsersContext _dbContext;

        public UnitOfWork
        (
            UsersContext dbContext
        )
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
