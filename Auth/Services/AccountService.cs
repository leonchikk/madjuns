using Auth.Data.Entities;
using Auth.Data.Interfaces;
using Auth.Interfaces;
using Auth.Models.Requests;
using Common.Core.Events;
using Common.Core.Helpers;
using EasyNetQ;
using System;
using System.Threading.Tasks;

namespace Auth.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBus _serviceBus;

        public AccountService(IUnitOfWork unitOfWork, IBus serviceBus)
        {
            _unitOfWork = unitOfWork;
            _serviceBus = serviceBus;
        }

        public async Task<Account> CreateUserAsync(CreateAccountRequest request)
        {
            var newAccount = new Account
            {
                Id = Guid.NewGuid(),
                Age = request.Age,
                BirthDay = request.BirthDay,
                Email = request.Email,
                Password = CryptographyHelper.EncryptString(request.Password),
                UserName = request.UserName
            };

            await _unitOfWork.AccountsRepository.AddAsync(newAccount);
            await _unitOfWork.SaveAsync();

            await _serviceBus.PublishAsync
            (
                new UserCreatedEvent
                {
                    UserId = newAccount.Id,
                    Email = newAccount.Email,
                    Password = newAccount.Password,
                    UserName = newAccount.UserName
                }
            );

            return newAccount;
        }
    }
}
