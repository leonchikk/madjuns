using Authentication.Data.Entities;
using Authentication.Data.Interfaces;
using Authentication.Interfaces;
using Authentication.Models.Requests;
using Authentication.Models.Responses;
using Common.Core.Events;
using EasyNetQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBus _serviceBus;

        public AuthenticationService(IUnitOfWork unitOfWork, IBus serviceBus)
        {
            _unitOfWork = unitOfWork;
            _serviceBus = serviceBus;

            _serviceBus.Subscribe<UserCreatedEvent>(Guid.NewGuid().ToString(), async msg => await AddNewUserAsync(msg));
        }

        public async Task<AuthenticationToken> LoginAsync(AuthenticationRequest request)
        {
            throw new NotImplementedException();
        }

        private async Task AddNewUserAsync(UserCreatedEvent createdEvent)
        {
            await _unitOfWork.UsersRepository.AddAsync(new User
            {
                Email = createdEvent.Email,
                Id = createdEvent.UserId,
                Password = createdEvent.Password,
                UserName = createdEvent.UserName
            });

            await _unitOfWork.SaveAsync();
        }
    }
}
