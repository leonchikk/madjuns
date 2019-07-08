using Authentication.Data.Entities;
using Common.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Data.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<User> UsersRepository { get; };
        Task SaveAsync();
    }
}
