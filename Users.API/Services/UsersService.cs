using AutoMapper;
using Common.Core.Events;
using Common.Core.Interfaces;
using EasyNetQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Users.API.Interfaces;
using Users.API.Models.Requests;
using Users.API.Models.Responses;
using Users.Core.Domain;
using UserProfile = Users.Core.Domain.Profile;

namespace Users.API.Services
{
    public class UsersService : IUsersService
    {
        public IUnitOfWork UnitOfWork { get; set; }
        public IBus ServiceBus { get; set; }
        public IMapper Mapper { get; set; }

        private IRepository<User> UsersRepository { get; set; }

        public UsersService(IUnitOfWork unitOfWork, IBus serviceBus, IMapper mapper, IRepository<User> usersRepository)
        {
            UnitOfWork = unitOfWork;
            ServiceBus = serviceBus;
            Mapper = mapper;
            UsersRepository = usersRepository;
        }

        public async Task DeleteUserAsync(Guid id)
        {
            User user = UsersRepository.FindBy(u => u.Id == id).FirstOrDefault();

            if (user == null)
            {
                throw new Exception("User with that account id does not exist");
            }

            await ServiceBus.PublishAsync(new UserDeletedEvent
            {
                AcountId = id
            });

            user.IsDeleted = true;
            await UnitOfWork.SaveChangesAsync();
        }

        public UserResponseModel GetUserById(Guid id)
        {
            User user = UsersRepository.FindBy(u => u.Id == id).FirstOrDefault();

            if (user == null)
            {
                throw new Exception("User with that id does not exist");
            }

            return Mapper.Map<UserResponseModel>(user);
        }

        public ProfileResponseModel GetUserProfile(Guid id)
        {
            User user = UsersRepository.FindBy(u => u.Id == id).FirstOrDefault();

            if (user == null)
            {
                throw new Exception("User with that id does not exist");
            }

            return Mapper.Map<ProfileResponseModel>(user.Profile);
        }

        public IEnumerable<BaseUserResponseModel> GetUsers()
        {
            var users = UsersRepository.GetAll();
            
            return Mapper.Map<IEnumerable<UserResponseModel>>(users);
        }

        public IEnumerable<SettingResponseModel> GetUserSettings(Guid id)
        {
            User user = UsersRepository.FindBy(u => u.Id == id).FirstOrDefault();

            if (user == null)
            {
                throw new Exception("User with that id does not exist");
            }

            return Mapper.Map<IEnumerable<SettingResponseModel>>(user.Settings);
        }

        public async Task<UserResponseModel> UpdateUserAsync(Guid id, UpdateUserRequest request)
        {
            User user = UsersRepository.FindBy(u => u.Id == id).FirstOrDefault();

            if (user == null)
            {
                throw new Exception("User with that id does not exist");
            }

            UserProfile profile = Mapper.Map<UserProfile>(request.Profile);
            user.Update(profile);

            await UnitOfWork.SaveChangesAsync();

            return Mapper.Map<UserResponseModel>(user);
        }

        public async Task<UserResponseModel> CreateUserAsync(UserCreatedEvent createdEvent)
        {
            User user = new User(createdEvent.UserId, new UserProfile()
            {
                Id = Guid.NewGuid(),
                Email = createdEvent.Email,
                UserName = createdEvent.UserName
            });

            UsersRepository.Add(user);

            await UnitOfWork.SaveChangesAsync();

            return Mapper.Map<UserResponseModel>(user);
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

        public async Task<UserResponseModel> AddToBlackListAsync(Guid currentUserId, Guid targetUserId)
        {
            var currentUser = await UsersRepository.FirstOrDefaultAsync(u => u.Id == currentUserId);
            var targetUser = await UsersRepository.FirstOrDefaultAsync(u => u.Id == targetUserId);

            currentUser.AddToBlackList(targetUser);
            await UnitOfWork.SaveChangesAsync();

            return Mapper.Map<UserResponseModel>(currentUser);
        }

        public async Task<UserResponseModel> SendRequestToBeFriendAsync(Guid currentUserId, Guid targetUserId)
        {
            var currentUser = await UsersRepository.FirstOrDefaultAsync(u => u.Id == currentUserId);
            var targetUser = await UsersRepository.FirstOrDefaultAsync(u => u.Id == targetUserId);

            currentUser.SubscribeTo(targetUser);
            await UnitOfWork.SaveChangesAsync();

            return Mapper.Map<UserResponseModel>(currentUser);
        }

        public IEnumerable<BaseUserResponseModel> GetUserFriends(Guid userId)
        {
            var userFriends = UsersRepository.FindBy(u => u.Id == userId, u => u.UserFriends).SelectMany(u => u.UserFriends);
            return Mapper.Map<IEnumerable<BaseUserResponseModel>>(userFriends);
        }

        public IEnumerable<BaseUserResponseModel> GetUserSubscribers(Guid userId)
        {
            var userSubscribers = UsersRepository.FindBy(u => u.Id == userId, u => u.Subscribers).SelectMany(u => u.Subscribers);
            return Mapper.Map<IEnumerable<BaseUserResponseModel>>(userSubscribers);
        }

        public IEnumerable<BaseUserResponseModel> GetUserBlackList(Guid userId)
        {
            var userBlackList = UsersRepository.FindBy(u => u.Id == userId, u => u.BlackList).SelectMany(u => u.BlackList);
            return Mapper.Map<IEnumerable<BaseUserResponseModel>>(userBlackList);
        }

        public Task RejectSubscription(Guid currentUserId, Guid targetUserId)
        {
            throw new NotImplementedException();
        }
    }
}
