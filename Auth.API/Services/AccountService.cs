using Auth.Core.Entities;
using Auth.Core.Enumerations;
using Auth.Core.Interfaces;
using Auth.API.Interfaces;
using Auth.API.Models.Requests;
using Common.Core.Events;
using Common.Core.Helpers;
using EasyNetQ;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.API.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBus _serviceBus;
        private readonly IHttpContextAccessor _accessor;

        public AccountService(IUnitOfWork unitOfWork, IBus serviceBus, IHttpContextAccessor accessor)
        {
            _accessor = accessor;
            _unitOfWork = unitOfWork;
            _serviceBus = serviceBus;
        }

        public async Task<Account> CreateUserAsync(CreateAccountRequest request)
        {
            var isAccountExist = _unitOfWork.AccountsRepository.Any(a => a.Email == request.Email);

            if (isAccountExist)
            {
                throw new Exception("Account with that email already exists!");
            }

            var newAccount = new Account(request.Email, request.Password, request.UserName, request.BirthDay, SystemRoles.User);

            await _unitOfWork.AccountsRepository.AddAsync(newAccount);
            await _unitOfWork.SaveAsync();

            string callbackUrl = UrlHelper.AddUrlParameters(
                url: $"{_accessor.HttpContext.Request.Scheme}://{_accessor.HttpContext.Request.Host}/api/auth/verify-email",
                parameters: new Dictionary<string, string>
                {
                    { "token", newAccount.VerifyEmailToken },
                    { "redirectUrl", request.RedirectUrl }
                });

            await PublishEmailEvent("Verify account", newAccount.Email, callbackUrl);

            await _serviceBus.PublishAsync
            (
                new UserCreatedEvent
                {
                    Email = newAccount.Email,
                    UserId = newAccount.Id,
                    UserName = newAccount.UserName
                }
            );

            return newAccount;
        }

        public async Task DeleteUserAsync(Guid id)
        {
            var account = _unitOfWork.AccountsRepository.FindBy(a => a.Id == id).FirstOrDefault();

            if (account == null)
            {
                throw new Exception("Account with that email does not exist!");
            }

            account.IsDeleted = true;
            await _unitOfWork.SaveAsync();
        }

        public async Task ForgotPasswordAsync(ForgotPasswordRequest request)
        {
            var account = _unitOfWork.AccountsRepository.FindBy(a => a.Email == request.Email).FirstOrDefault();

            if (account == null)
            {
                throw new Exception("Account with that email does not exist!");
            }

            account.GenerateForgotPasswordToken();
            await _unitOfWork.SaveAsync();

            string callbackUrl = UrlHelper.AddUrlParameters(
               url: request.RedirectUrl,
               parameters: new Dictionary<string, string>
               {
                    { "token", account.ForgotPasswordToken }
               });

            await PublishEmailEvent("Change password", account.Email, callbackUrl);
        }

        public async Task ResetPasswordAsync(ResetPasswordRequest request)
        {
            var account = _unitOfWork.AccountsRepository.FindBy(a => a.ForgotPasswordToken == request.ForgotPasswordToken).FirstOrDefault();

            if (account == null)
            {
                throw new Exception("Account with that token does not exist!");
            }

            account.ChangePassword(request.Password);
            await _unitOfWork.SaveAsync();
        }

        public async Task VerifyEmailAsync(VerifyEmailRequest request)
        {
            var account = _unitOfWork.AccountsRepository.FindBy(a => a.VerifyEmailToken == request.Token).FirstOrDefault();

            if (account == null)
            {
                throw new Exception("Account does not exist!");
            }

            if (account.IsEmailVerified)
            {
                throw new Exception("Account has been verified already!");
            }

            account.VerifyEmail();
            await _unitOfWork.SaveAsync();
        }

        private async Task PublishEmailEvent(string subject, string to, string body)
        {
            await _serviceBus.PublishAsync
            (
                new SendMailEvent
                {
                    Subject = subject,
                    To = to,
                    Body = body
                }
            );
        }
    }
}
