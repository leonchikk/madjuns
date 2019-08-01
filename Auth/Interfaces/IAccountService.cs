using Auth.Data.Entities;
using Auth.Models.Requests;
using System.Threading.Tasks;

namespace Auth.Interfaces
{
    public interface IAccountService
    {
        Task<Account> CreateUserAsync(CreateAccountRequest request);
        Task VerifyEmailAsync(VerifyEmailRequest request);
        Task ForgotPasswordAsync(ForgotPasswordRequest request);
        Task ResetPasswordAsync(ResetPasswordRequest request);
    }
}
