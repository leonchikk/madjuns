using Common.Core.Interfaces;
using Common.Messaging.Abstractions;
using System;
using System.Linq;
using System.Threading.Tasks;
using Users.Core.Domain;
using Users.Core.Events;
using Users.Services.Users.Interfaces;
using UserProfile = Users.Core.Domain.Profile;

namespace Users.Services.Services
{
    //TODO Make settings service and probably profile
    public class UsersService : IUsersService
    {
        public IUnitOfWork UnitOfWork { get; set; }
        public IEventBus ServiceBus { get; set; }

        private IRepository<User> UsersRepository { get; set; }

        public UsersService(IUnitOfWork unitOfWork, IEventBus serviceBus, IRepository<User> usersRepository)
        {
            UnitOfWork = unitOfWork;
            ServiceBus = serviceBus;
            UsersRepository = usersRepository;
        }

        public async Task DeleteUserAsync(Guid id)
        {
            User user = UsersRepository.FindBy(u => u.Id == id).FirstOrDefault();

            if (user == null)
            {
                throw new Exception("User with that account id does not exist");
            }

            ServiceBus.Publish(new UserDeletedEvent
            {
                AccountId = id
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

        public async Task<User> UpdateUserAsync(Guid id, Profile profile)
        {
            User user = UsersRepository.FindBy(u => u.Id == id).FirstOrDefault();

            if (user == null)
            {
                throw new Exception("User with that id does not exist");
            }

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
