using AutoMapper;
using Common.Core.Events;
using EasyNetQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Users.API.Interfaces;
using Users.API.Models.Requests;
using Users.API.Models.Responses;
using Users.Core.Domain;
using Users.Core.Interfaces;
using UserProfile = Users.Core.Domain.Profile;

namespace Users.API.Services
{
    public class UsersService : IUsersService
    {
        public IUnitOfWork UnitOfWork { get; set; }
        public IBus ServiceBus { get; set; }
        public IMapper Mapper { get; set; }

        public UsersService(IUnitOfWork unitOfWork, IBus serviceBus, IMapper mapper)
        {
            UnitOfWork = unitOfWork;
            ServiceBus = serviceBus;
            Mapper = mapper;
        }

        public async Task DeleteUserAsync(Guid id)
        {
            //TODO Make UserDeleteEvent
            await UnitOfWork.UsersRepository.DeleteAsync(id);
            await UnitOfWork.SaveAsync();
        }

        public UserResponseModel GetUserById(Guid id)
        {
            var user = UnitOfWork.UsersRepository.FindBy(u => u.Id == id).FirstOrDefault();

            if (user == null)
            {
                throw new Exception("User with that id does not exist");
            }

            return Mapper.Map<UserResponseModel>(user);
        }

        public ProfileResponseModel GetUserProfile(Guid id)
        {
            var user = UnitOfWork.UsersRepository.FindBy(u => u.Id == id).FirstOrDefault();

            if (user == null)
            {
                throw new Exception("User with that id does not exist");
            }

            return Mapper.Map<ProfileResponseModel>(user.Profile);
        }

        public IEnumerable<UserResponseModel> GetUsers()
        {
            var users = UnitOfWork.UsersRepository.GetAll();

            return Mapper.Map<IEnumerable<UserResponseModel>>(users);
        }

        public IEnumerable<SettingResponseModel> GetUserSettings(Guid id)
        {
            var user = UnitOfWork.UsersRepository.FindBy(u => u.Id == id).FirstOrDefault();

            if (user == null)
            {
                throw new Exception("User with that id does not exist");
            }

            return Mapper.Map<IEnumerable<SettingResponseModel>>(user.Settings);
        }

        public async Task<UserResponseModel> UpdateUserAsync(Guid id, UpdateUserRequest request)
        {
            var user = UnitOfWork.UsersRepository.FindBy(u => u.Id == id).FirstOrDefault();

            if (user == null)
            {
                throw new Exception("User with that id does not exist");
            }

            var profile = Mapper.Map<UserProfile>(request.Profile);
            user.Update(profile);

            await UnitOfWork.SaveAsync();

            return Mapper.Map<UserResponseModel>(user);
        }

        public async Task<UserResponseModel> CreateUserAsync(UserCreatedEvent createdEvent)
        {
            var user = new User(createdEvent.UserId, new UserProfile()
            {
                Email = createdEvent.Email,
                UserName = createdEvent.UserName
            });

            UnitOfWork.UsersRepository.Add(user);

            await UnitOfWork.SaveAsync();

            return Mapper.Map<UserResponseModel>(user);
        }
    }
}
