using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Common.Core.Interfaces;
using EasyNetQ;
using Users.Core.Domain;
using Users.Services.Users.Models.Responses;

namespace Users.Services.Services.Subscriptions
{
    //TODO Move models from users service
    public class SubscriptionsService : ISubscriptionsService
    {
        public IUnitOfWork UnitOfWork { get; set; }
        public IBus ServiceBus { get; set; }
        public IMapper Mapper { get; set; }

        private IRepository<User> UsersRepository { get; set; }

        public SubscriptionsService(IUnitOfWork unitOfWork, IBus serviceBus, IMapper mapper, IRepository<User> usersRepository)
        {
            UnitOfWork = unitOfWork;
            ServiceBus = serviceBus;
            Mapper = mapper;
            UsersRepository = usersRepository;
        }

        public async Task<UserResponseModel> SendRequestToBeFriendAsync(Guid currentUserId, Guid targetUserId)
        {
            var currentUser = await UsersRepository.FirstOrDefaultAsync(u => u.Id == currentUserId);
            var targetUser = await UsersRepository.FirstOrDefaultAsync(u => u.Id == targetUserId);

            currentUser.SubscribeTo(targetUser);
            await UnitOfWork.SaveChangesAsync();

            return Mapper.Map<UserResponseModel>(currentUser);
        }

        public IEnumerable<BaseUserResponseModel> GetUserSubscribers(Guid userId)
        {
            var userSubscribers = UsersRepository.FindBy(u => u.Id == userId, u => u.Subscribers).SelectMany(u => u.Subscribers);
            return Mapper.Map<IEnumerable<BaseUserResponseModel>>(userSubscribers);
        }

        public async Task RejectSubscription(Guid currentUserId, Guid targetUserId)
        {
            var targetUser = await UsersRepository.FirstOrDefaultAsync(u => u.Id == targetUserId);

            targetUser.RejectSubscription(currentUserId);
            await UnitOfWork.SaveChangesAsync();
        }
    }
}
