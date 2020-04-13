using System.Threading;
using System.Threading.Tasks;
using Common.Core.Interfaces;
using Communication.Core.Entities;
using MediatR;

namespace Communication.API.Application.Commands.Users
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<User> _usersRepository;

        public CreateUserCommandHandler(IUnitOfWork unitOfWork, IRepository<User> usersRepository)
        {
            _unitOfWork = unitOfWork;
            _usersRepository = usersRepository;
        }

        public async Task<bool> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = new User(request.UserId, request.UserName);

            await _usersRepository.AddAsync(user);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }
    }
}
