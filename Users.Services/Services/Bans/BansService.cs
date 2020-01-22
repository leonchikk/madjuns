﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Common.Core.Interfaces;
using Common.Messaging.Abstractions;
using Users.Core.Domain;

namespace Users.Services.Services.Bans
{
    public class BansService : IBansService
    {
        public IUnitOfWork UnitOfWork { get; set; }
        public IEventBus ServiceBus { get; set; }

        private IRepository<User> UsersRepository { get; set; }

        public BansService(IUnitOfWork unitOfWork, IEventBus serviceBus, IRepository<User> usersRepository)
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

            return targetUser;
        }

        public IQueryable<BlockedUser> GetUserBlackList(Guid userId)
        {
            return UsersRepository.FindBy(u => u.Id == userId, u => u.UsersBlockedByMe).SelectMany(u => u.UsersBlockedByMe);
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
