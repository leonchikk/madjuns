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

namespace Users.Services.Services.Friends
{
    public class FriendsService : IFriendsService
    {
        public IUnitOfWork UnitOfWork { get; set; }
        public IBus ServiceBus { get; set; }
        public IMapper Mapper { get; set; }

        private IRepository<User> UsersRepository { get; set; }

        public FriendsService(IUnitOfWork unitOfWork, IBus serviceBus, IMapper mapper, IRepository<User> usersRepository)
        {
            UnitOfWork = unitOfWork;
            ServiceBus = serviceBus;
            Mapper = mapper;
            UsersRepository = usersRepository;
        }

        public async Task<UserResponseModel> AddToFriendAsync(Guid currentUserId, Guid subscriberId)
        {
            var currentUser = await UsersRepository.FirstOrDefaultAsync(u => u.Id == currentUserId);
            var subscriber = await UsersRepository.FirstOrDefaultAsync(u => u.Id == subscriberId);

            currentUser.AddToFriends(subscriber);
            await UnitOfWork.SaveChangesAsync();

            return Mapper.Map<UserResponseModel>(currentUser);
        }

        public async Task<UserResponseModel> RemoveFriendAsync(Guid currentUserId, Guid friendId)
        {
            var currentUser = await UsersRepository.FirstOrDefaultAsync(u => u.Id == currentUserId);
            var friend = await UsersRepository.FirstOrDefaultAsync(u => u.Id == friendId);

            currentUser.RemoveFromFriends(friend);
            await UnitOfWork.SaveChangesAsync();

            return Mapper.Map<UserResponseModel>(currentUser);
        }

        public IEnumerable<BaseUserResponseModel> GetUserFriends(Guid userId)
        {
            var userFriends = UsersRepository.FindBy(u => u.Id == userId, u => u.UserFriends).SelectMany(u => u.UserFriends);
            return Mapper.Map<IEnumerable<BaseUserResponseModel>>(userFriends);
        }
    }
}
