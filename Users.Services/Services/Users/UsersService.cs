using Common.Core.Events;
using Common.Core.Interfaces;
using EasyNetQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Users.Core.Domain;
using Users.Services.Users.Interfaces;
using Users.Services.Models.Requests;
using Users.Services.Models.Responses;
using UserProfile = Users.Core.Domain.Profile;
using AutoMapper;

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

        public User GetUserById(Guid id)
        {
            User user = UsersRepository.FindBy(u => u.Id == id).FirstOrDefault();

            if (user == null)
            {
                throw new Exception("User with that id does not exist");
            }

            return user;
        }

        public UserProfile GetUserProfile(Guid id)
        {
            User user = UsersRepository.FindBy(u => u.Id == id).FirstOrDefault();

            if (user == null)
            {
                throw new Exception("User with that id does not exist");
            }

            return user.Profile;
        }

        public IQueryable<User> GetUsers()
        {
            return UsersRepository.GetAll();
        }

        public IQueryable<UserSetting> GetUserSettings(Guid id)
        {
            User user = UsersRepository.FindBy(u => u.Id == id).FirstOrDefault();

            if (user == null)
            {
                throw new Exception("User with that id does not exist");
            }

            return user.Settings.AsQueryable();
        }

        public async Task<User> UpdateUserAsync(Guid id, UpdateUserRequest request)
        {
            User user = UsersRepository.FindBy(u => u.Id == id).FirstOrDefault();

            if (user == null)
            {
                throw new Exception("User with that id does not exist");
            }

            UserProfile profile = Mapper.Map<UserProfile>(request.Profile);
            user.Update(profile);

            await UnitOfWork.SaveChangesAsync();

            return user;
        }

        public async Task<User> CreateUserAsync(UserCreatedEvent createdEvent)
        {
            User user = new User(createdEvent.UserId, new UserProfile()
            {
                Id = Guid.NewGuid(),
                Email = createdEvent.Email,
                UserName = createdEvent.UserName
            });

            UsersRepository.Add(user);

            await UnitOfWork.SaveChangesAsync();

            return user;
        }
    }
}
