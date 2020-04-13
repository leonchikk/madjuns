using System;
using System.Threading.Tasks;
using Common.Messaging.Abstractions;
using Communication.API.Application.Commands.Users;
using Communication.Core.Events;
using MediatR;

namespace Communication.API.Application.EventHandlers
{
    public class UserDeletedEventHandler : IEventHandler<UserDeletedEvent>
    {
        private readonly IMediator _mediator;

        public UserDeletedEventHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task HandleAsync(UserDeletedEvent @event)
        {
            var deleteUserCommand = new DeleteUserCommand()
            {
                UserId = @event.AccountId
            };

            var result = await _mediator.Send(deleteUserCommand);

            if (result)
                throw new ApplicationException($"Cannot delete user with id = {@event.AccountId}");
        }
    }
}
