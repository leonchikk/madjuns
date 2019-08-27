using Common.Core.Events;
using EasyNetQ;
using Microsoft.Extensions.DependencyInjection;
using System;
using Users.API.Interfaces;

namespace Users.API
{
    public class ServiceBusListener : IDisposable
    {
        private readonly IBus _serviceBus;
        private readonly IServiceProvider _serviceProvider;
        private IUsersService _usersService;

        private IServiceScope _serviceScope;

        public ServiceBusListener
        (
            IBus serviceBus,
            IServiceProvider serviceProvider
        )
        {
            _serviceBus = serviceBus;
            _serviceProvider = serviceProvider;
        }

        public void SubscribeToEvents()
        {
            _serviceScope = _serviceProvider.CreateScope();
            _usersService = (IUsersService)_serviceScope.ServiceProvider.GetService(typeof(IUsersService));
            _serviceBus.Subscribe<UserCreatedEvent>(Guid.NewGuid().ToString(), async msg => await _usersService.CreateUserAsync(msg));
        }

        public void Dispose()
        {
            _serviceBus.Dispose();
            _serviceScope.Dispose();
        }
    }
}
