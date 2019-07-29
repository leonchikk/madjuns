using Auth.Data.Entities;
using Common.Core.Interfaces;
using System;
using System.Threading.Tasks;

namespace Auth.Data.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Account> AccountsRepository { get; }
        Task SaveAsync();
    }
}
