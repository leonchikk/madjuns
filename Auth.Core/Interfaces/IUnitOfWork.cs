using Auth.Core.Entities;
using Common.Core.Interfaces;
using System;
using System.Threading.Tasks;

namespace Auth.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Account> AccountsRepository { get; }
        Task SaveAsync();
    }
}
