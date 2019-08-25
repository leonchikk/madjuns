using Auth.API.Interfaces;
using Common.Core.Events;
using EasyNetQ;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Auth.API
{
    public class ServiceBusListener : IDisposable
    {
        private IAccountService _accountService;
        private readonly IBus _serviceBus;
        private readonly IServiceProvider _serviceProvider;

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
            _accountService = (IAccountService)_serviceScope.ServiceProvider.GetService(typeof(IAccountService));
            _serviceBus.Subscribe<UserDeletedEvent>(Guid.NewGuid().ToString(), async msg => await _accountService.DeleteUserAsync(msg.AcountId));
        }

        public void Dispose()
        {
            _serviceBus.Dispose();
            _serviceScope.Dispose();
        }
    }
}
