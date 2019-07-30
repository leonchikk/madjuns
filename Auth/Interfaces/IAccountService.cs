using Auth.Data.Entities;
using Auth.Models.Requests;
using System.Threading.Tasks;

namespace Auth.Interfaces
{
    public interface IAccountService
    {
        Task<Account> CreateUserAsync(CreateAccountRequest request);
    }
}
