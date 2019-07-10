using Authentication.Data.Entities;
using Authentication.Data.Interfaces;
using Common.Core.Events;
using EasyNetQ;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Authentication.Services
{
    public class ServiceBusListener : IDisposable
    {
        private readonly IBus _serviceBus;
        private readonly IServiceProvider _serviceProvider;
        private IServiceScope _serviceScope;
        private IUnitOfWork _unitOfWork;

        public ServiceBusListener(IBus serviceBus, IServiceProvider serviceProvider)
        {
            _serviceBus = serviceBus;
            _serviceProvider = serviceProvider;
        }

        public void SubscribeToEvents()
        {
            _serviceScope = _serviceProvider.CreateScope();

            _unitOfWork = (IUnitOfWork)_serviceScope.ServiceProvider.GetService(typeof(IUnitOfWork));
            _serviceBus.Subscribe<UserCreatedEvent>(Guid.NewGuid().ToString(), async msg => await AddNewUserAsync(msg));

        }

        public void Dispose()
        {
            _serviceBus.Dispose();
            _serviceScope.Dispose();
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
