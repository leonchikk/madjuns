using Auth.Data.Entities;
using Auth.Data.Interfaces;
using Auth.Interfaces;
using Auth.Models.Requests;
using Common.Core.Events;
using Common.Core.Helpers;
using EasyNetQ;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.Services
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
                throw new Exception("Account with that email already exists!");

            var newAccount = new Account(request.Email, request.Password, request.UserName, request.BirthDay);

            await _unitOfWork.AccountsRepository.AddAsync(newAccount);
            await _unitOfWork.SaveAsync();

            var callbackUrl = UrlHelper.AddUrlParameters(
                url: $"{_accessor.HttpContext.Request.Scheme}://{_accessor.HttpContext.Request.Host}/api/auth/verify-email",
                parameters: new Dictionary<string, string>
                {
                    { "token", newAccount.VerifyEmailToken },
                    { "redirectUrl", request.RedirectUrl }
                });

            await _serviceBus.PublishAsync
            (
                new SendMailEvent
                {
                    To = request.Email,
                    Body = callbackUrl
                }
            );

            return newAccount;
        }

        public async Task VerifyEmailAsync(VerifyEmailRequest request)
        {
            var account = _unitOfWork.AccountsRepository.FindBy(a => a.VerifyEmailToken == request.Token).FirstOrDefault();

            if(account == null)
                throw new Exception("Account does not exist!");

            if (account.IsEmailVerified)
                throw new Exception("Account has been verified already!");

            account.VerifyEmail();
            await _unitOfWork.SaveAsync();
        }
    }
}
