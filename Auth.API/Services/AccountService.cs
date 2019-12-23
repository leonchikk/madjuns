using Auth.API.Interfaces;
using Auth.API.Models.Requests;
using Auth.Core.Entities;
using Auth.Core.Enumerations;
using Auth.Core.Events;
using Common.Core.Helpers;
using Common.Core.Interfaces;
using Common.Messaging.Abstractions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.API.Services
{
    public class AccountService : IAccountService
    {
        private readonly IRepository<Account> _accountsRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEventBus _serviceBus;
        private readonly IHttpContextAccessor _accessor;

        public AccountService(IRepository<Account> accountsRepository, IUnitOfWork unitOfWork, IEventBus serviceBus, IHttpContextAccessor accessor)
        {
            _accountsRepository = accountsRepository;
            _accessor = accessor;
            _unitOfWork = unitOfWork;
            _serviceBus = serviceBus;
        }

        public async Task<Account> CreateUserAsync(CreateAccountRequest request)
        {
            bool isAccountExist = _accountsRepository.Any(a => a.Email == request.Email);

            if (isAccountExist)
            {
                throw new Exception("Account with that email already exists!");
            }

            Account newAccount = new Account(request.Email, request.Password, request.UserName, request.BirthDay, SystemRoles.User);

            await _accountsRepository.AddAsync(newAccount);
            await _unitOfWork.SaveChangesAsync();

            string callbackUrl = UrlHelper.AddUrlParameters(
                url: $"{_accessor.HttpContext.Request.Headers["X-ApiGateway-Address"]}/api/auth/verify-email",
                parameters: new Dictionary<string, string>
                {
                    { "token", newAccount.VerifyEmailToken },
                    { "redirectUrl", request.RedirectUrl }
                });

            PublishEmailEvent("Verify account", newAccount.Email, callbackUrl);

            _serviceBus.Publish
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
            Account account = _accountsRepository.FindBy(a => a.Id == id).FirstOrDefault();

            if (account == null)
            {
                throw new Exception("Account with that email does not exist!");
            }

            account.IsDeleted = true;
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task ForgotPasswordAsync(ForgotPasswordRequest request)
        {
            Account account = _accountsRepository.FindBy(a => a.Email == request.Email).FirstOrDefault();

            if (account == null)
            {
                throw new Exception("Account with that email does not exist!");
            }

            account.GenerateForgotPasswordToken();
            await _unitOfWork.SaveChangesAsync();

            string callbackUrl = UrlHelper.AddUrlParameters(
               url: request.RedirectUrl,
               parameters: new Dictionary<string, string>
               {
                    { "token", account.ForgotPasswordToken }
               });

             PublishEmailEvent("Change password", account.Email, callbackUrl);
        }

        public async Task ResetPasswordAsync(ResetPasswordRequest request)
        {
            Account account = _accountsRepository.FindBy(a => a.ForgotPasswordToken == request.ForgotPasswordToken).FirstOrDefault();

            if (account == null)
            {
                throw new Exception("Account with that token does not exist!");
            }

            account.ChangePassword(request.Password);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task VerifyEmailAsync(VerifyEmailRequest request)
        {
            Account account = _accountsRepository.FindBy(a => a.VerifyEmailToken == request.Token).FirstOrDefault();

            if (account == null)
            {
                throw new Exception("Account does not exist!");
            }

            if (account.IsEmailVerified)
            {
                throw new Exception("Account has been verified already!");
            }

            account.VerifyEmail();
            await _unitOfWork.SaveChangesAsync();
        }

        private void PublishEmailEvent(string subject, string to, string body)
        {
            _serviceBus.Publish
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
