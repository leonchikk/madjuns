using Common.Core.Events;
using EasyNetQ;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Users.API.Interfaces;

namespace Users.API
{
    public class ServiceBusListener : IDisposable
    {
        private readonly IBus _serviceBus;
        private readonly IServiceProvider _serviceProvider;
        private readonly IUsersService _usersService;

        private IServiceScope _serviceScope;

        public ServiceBusListener
        (
            IBus serviceBus, 
            IServiceProvider serviceProvider,
            IUsersService usersService
        )
        {
            _serviceBus = serviceBus;
            _serviceProvider = serviceProvider;
            _usersService = usersService;
        }

        public void SubscribeToEvents()
        {
            _serviceScope = _serviceProvider.CreateScope();
            _serviceBus.Subscribe<UserCreatedEvent>(Guid.NewGuid().ToString(), async msg => await _usersService.CreateUserAsync(msg));
        }

        public void Dispose()
        {
            _serviceBus.Dispose();
            _serviceScope.Dispose();
        }
    }
}
