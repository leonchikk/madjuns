using Common.Core.Interfaces;
using Communication.Core.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Communication.API.Application.Commands.Users
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<User> _usersRepository;

        public DeleteUserCommandHandler(IUnitOfWork unitOfWork, IRepository<User> usersRepository)
        {
            _unitOfWork = unitOfWork;
            _usersRepository = usersRepository;

        }
        public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _usersRepository.FirstOrDefaultAsync(x => x.Id == request.UserId);

            if (user == null)
                return false;

            user.Delete();

            await _unitOfWork.SaveChangesAsync();

            return true;
        }
    }
}
