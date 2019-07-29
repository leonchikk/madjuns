using Auth.Models.Requests;
using System.Threading.Tasks;

namespace Auth.Interfaces
{
    public interface IAccountService
    {
        Task CreateUserAsync(CreateUserRequest request);
    }
}
