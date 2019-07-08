using Accounts.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Accounts.Interfaces
{
    public interface IAccountService
    {
        Task CreateUserAsync(CreateUserRequest request);
    }
}
