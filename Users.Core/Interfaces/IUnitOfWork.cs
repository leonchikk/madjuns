using Common.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Users.Core.Domain;

namespace Users.Core.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<User> UsersRepository { get; }
        Task SaveAsync();
    }
}
