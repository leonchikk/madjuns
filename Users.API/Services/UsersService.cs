using EasyNetQ;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Users.API.Interfaces;
using Users.API.Models.Requests;
using Users.API.Models.Responses;
using Users.Core.Interfaces;

namespace Users.API.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBus _serviceBus;

        public UsersService(IUnitOfWork unitOfWork, IBus serviceBus)
        {
            _unitOfWork = unitOfWork;
            _serviceBus = serviceBus;
        }

        public async Task DeleteUserAsync(Guid id)
        {
            await _unitOfWork.UsersRepository.DeleteAsync(id);
        }

        public async Task<UserResponseModel> GetUserByIdAsync(Guid id)
        {
            var user = _unitOfWork.UsersRepository.FindBy(u => u.Id == id);

            if (user == null)
                throw new Exception("User with that id does not exist");

            //TODO Make mapper
            return new UserResponseModel();
        }

        public async Task<ProfileResponseModel> GetUserProfileAsync(Guid id)
        {
            var user = _unitOfWork.UsersRepository.FindBy(u => u.Id == id);

            if (user == null)
                throw new Exception("User with that id does not exist");

            //TODO Make mapper
            return new ProfileResponseModel();
        }

        public async Task<IEnumerable<UserResponseModel>> GetUsers()
        {
            var users = _unitOfWork.UsersRepository.GetAll();

            //TODO Make mapper
            throw new NotImplementedException();
        }

        public async Task<ProfileResponseModel> GetUserSettingsAsync(Guid id)
        {
            var user = _unitOfWork.UsersRepository.FindBy(u => u.Id == id);

            if (user == null)
                throw new Exception("User with that id does not exist");

            //TODO Make mapper
            return new ProfileResponseModel();
        }

        public async Task<UserResponseModel> UpdateUserAsync(Guid id, UpdateUserRequest request)
        {
            var user = _unitOfWork.UsersRepository.FindBy(u => u.Id == id);

            if (user == null)
                throw new Exception("User with that id does not exist");

            //TODO User update method
            //TODO Make mapper

            throw new NotImplementedException();
        }
    }
}
