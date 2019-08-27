using Auth.API.Models.Requests;
using Auth.Core.Entities;
using System;
using System.Threading.Tasks;

namespace Auth.API.Interfaces
{
    public interface IAccountService
    {
        Task<Account> CreateUserAsync(CreateAccountRequest request);
        Task DeleteUserAsync(Guid id);
        Task VerifyEmailAsync(VerifyEmailRequest request);
        Task ForgotPasswordAsync(ForgotPasswordRequest request);
        Task ResetPasswordAsync(ResetPasswordRequest request);
    }
}
