using Authentication.Data.Entities;
using Authentication.Data.Interfaces;
using Authentication.Interfaces;
using Common.Core.Events;
using EasyNetQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.Services
{
    public class ServiceBusListener: IDisposable
    {
        private readonly IBus _serviceBus;
        private readonly IUnitOfWork _unitOfWork;

        public ServiceBusListener(IBus serviceBus)
        {
            _serviceBus = serviceBus;
        }

        public void SubscribeToEvents()
        {
            _serviceBus.Subscribe<UserCreatedEvent>(Guid.NewGuid().ToString(), async msg => await AddNewUserAsync(msg));
        }

        public void Dispose()
        {
            _serviceBus.Dispose();
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
