using MediatR;
using System;

namespace Communication.API.Application.Commands.Users
{
    public class DeleteUserCommand: IRequest<bool>
    {
        public Guid UserId { get; set; }
    }
}
