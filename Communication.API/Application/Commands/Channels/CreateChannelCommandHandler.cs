using System.Threading;
using System.Threading.Tasks;
using Common.Core.Interfaces;
using Communication.API.Models.ViewModels;
using Communication.API.Services;
using Communication.Core.Entities;
using MediatR;

namespace Communication.API.Application.Commands.Channels
{
    public class CreateChannelCommandHandler : IRequestHandler<CreateChannelCommand, ChannelViewModel>
    {
        private readonly IRepository<Channel> _channelRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenService _tokenService;

        public CreateChannelCommandHandler(
            IRepository<Channel> channelRepository,
            IUnitOfWork unitOfWork,
            ITokenService tokenService)
        {
            _channelRepository = channelRepository;
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
        }

        public async Task<ChannelViewModel> Handle(CreateChannelCommand request, CancellationToken cancellationToken)
        {
            var userId = _tokenService.GetUserId();

            var newChannel = new Channel()
            {
                Name = request.Name
            };

            newChannel.ChannelMembers.Add(new ChannelMember()
            {
                ChannelId = newChannel.Id,
                UserId = userId
            });

            await _channelRepository.AddAsync(newChannel);
            await _unitOfWork.SaveChangesAsync();

            return new ChannelViewModel()
            { 
                Name = newChannel.Name 
            };
        }
    }
}
