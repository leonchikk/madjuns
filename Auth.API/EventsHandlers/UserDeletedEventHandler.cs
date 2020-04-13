using System.Threading.Tasks;
using Auth.API.Interfaces;
using Auth.Core.Events;
using Common.Messaging.Abstractions;

namespace Auth.API.EventsHandlers
{
    public class UserDeletedEventHandler : IEventHandler<UserDeletedEvent>
    {
        private readonly IAccountService _accountService;

        public UserDeletedEventHandler(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public async Task HandleAsync(UserDeletedEvent @event)
        {
            await _accountService.DeleteUserAsync(@event.AccountId);
        }
    }
}
