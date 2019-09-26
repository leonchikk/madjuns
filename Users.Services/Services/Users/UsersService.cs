using AutoMapper;
using Common.Core.Events;
using Common.Core.Interfaces;
using EasyNetQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Users.Core.Domain;
using Users.Services.Users.Interfaces;
using Users.Services.Users.Models.Requests;
using Users.Services.Users.Models.Responses;
using UserProfile = Users.Core.Domain.Profile;

namespace Users.Services.Services
{
    //TODO Make settings service and probably profile
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
    }
}
