using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Common.Core.Interfaces;
using EasyNetQ;
using Users.Core.Domain;
using Users.Services.Models.Responses;

namespace Users.Services.Services.Bans
{
    public class BansService : IBansService
    {
        public IUnitOfWork UnitOfWork { get; set; }
        public IBus ServiceBus { get; set; }

        private IRepository<User> UsersRepository { get; set; }

        public BansService(IUnitOfWork unitOfWork, IBus serviceBus, IRepository<User> usersRepository)
        {
            UnitOfWork = unitOfWork;
            ServiceBus = serviceBus;
            UsersRepository = usersRepository;
        }

        public async Task<User> AddToBlackListAsync(Guid currentUserId, Guid targetUserId)
        {
            var currentUser = await UsersRepository.FirstOrDefaultAsync(u => u.Id == currentUserId);
            var targetUser = await UsersRepository.FirstOrDefaultAsync(u => u.Id == targetUserId);

            currentUser.AddToBlackList(targetUser);
            await UnitOfWork.SaveChangesAsync();

            return currentUser;
        }

        public IQueryable<BlockedUser> GetUserBlackList(Guid userId)
        {
            return UsersRepository.FindBy(u => u.Id == userId, u => u.BlackList).SelectMany(u => u.BlackList);
        }

        public async Task RemoveFromBlackList(Guid currentUserId, Guid targetUserId)
        {
            var currentUser = await UsersRepository.FirstOrDefaultAsync(u => u.Id == currentUserId);
            var targetUser = await UsersRepository.FirstOrDefaultAsync(u => u.Id == targetUserId);

            currentUser.RemoveFromBlackList(targetUser);
            await UnitOfWork.SaveChangesAsync();
        }
    }
}
