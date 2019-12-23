using Common.Messaging.Abstractions;
using System.Threading.Tasks;
using Users.Core.Events;
using Users.Services.Users.Interfaces;

namespace Users.API.EventHandlers
{
    public class UserCreatedEventHandler : IEventHandler<UserCreatedEvent>
    {
        private readonly IUsersService _usersService;

        public UserCreatedEventHandler(IUsersService usersService)
        {
            _usersService = usersService;
        }

        public async Task HandleAsync(UserCreatedEvent @event)
        {
            await _usersService.CreateUserAsync(@event);
        }
    }
}
