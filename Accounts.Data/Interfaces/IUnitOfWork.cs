using Accounts.Data.Entities;
using Common.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Accounts.Data.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Account> AccountsRepository { get; }
        Task SaveAsync();
    }
}
