using Common.Core.Interfaces;
using System;
using System.Threading.Tasks;

namespace Communication.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CommunicationsContext _dbContext;

        public UnitOfWork
        (
            CommunicationsContext dbContext
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
