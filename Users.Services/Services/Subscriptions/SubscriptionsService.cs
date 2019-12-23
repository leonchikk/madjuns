using Common.Core.Interfaces;
using Common.Messaging.Abstractions;
using System;
using System.Linq;
using System.Threading.Tasks;
using Users.Core.Domain;

namespace Users.Services.Services.Subscriptions
{
    public class SubscriptionsService : ISubscriptionsService
    {
        public IUnitOfWork UnitOfWork { get; set; }
        public IEventBus ServiceBus { get; set; }

        private IRepository<User> UsersRepository { get; set; }

        public SubscriptionsService(IUnitOfWork unitOfWork, IEventBus serviceBus, IRepository<User> usersRepository)
        {
            UnitOfWork = unitOfWork;
            ServiceBus = serviceBus;
            UsersRepository = usersRepository;
        }

        public async Task<User> SendRequestToBeFriendAsync(Guid currentUserId, Guid targetUserId)
        {
            var currentUser = await UsersRepository.FirstOrDefaultAsync(u => u.Id == currentUserId);
            var targetUser = await UsersRepository.FirstOrDefaultAsync(u => u.Id == targetUserId);

            currentUser.SubscribeTo(targetUser);
            await UnitOfWork.SaveChangesAsync();

            return currentUser;
        }

        public IQueryable<UserSubscriber> GetUserSubscribers(Guid userId)
        {
            return UsersRepository.FindBy(u => u.Id == userId, u => u.Subscribers).SelectMany(u => u.Subscribers);
        }

        public async Task RejectSubscription(Guid currentUserId, Guid targetUserId)
        {
            var targetUser = await UsersRepository.FirstOrDefaultAsync(u => u.Id == targetUserId);

            targetUser.RejectSubscription(currentUserId);
            await UnitOfWork.SaveChangesAsync();
        }
    }
}
