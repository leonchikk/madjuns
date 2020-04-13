using System;
using System.Threading.Tasks;
using Common.Messaging.Abstractions;
using Communication.API.Application.Commands.Users;
using Communication.Core.Events;
using MediatR;

namespace Communication.API.Application.EventHandlers
{
    public class UserCreatedEventHandler : IEventHandler<UserCreatedEvent>
    {
        private readonly IMediator _mediator;

        public UserCreatedEventHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task HandleAsync(UserCreatedEvent @event)
        {
            var createUserCommand = new CreateUserCommand()
            {
                UserId = @event.UserId,
                UserName = @event.UserName
            };

            var result = await _mediator.Send(createUserCommand);

            if (!result)
                throw new ApplicationException($"Cannot create user with id = {@event.Id}");
        }
    }
}
