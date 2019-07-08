using Common.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Accounts.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public Task SaveAsync()
        {
            throw new NotImplementedException();
        }
    }
}
