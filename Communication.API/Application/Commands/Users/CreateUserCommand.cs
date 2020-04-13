using MediatR;
using System;

namespace Communication.API.Application.Commands.Users
{
    public class CreateUserCommand: IRequest<bool>
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
    }
}
