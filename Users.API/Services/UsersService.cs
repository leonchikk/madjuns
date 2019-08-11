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

        public async Task DeleteUserAsync(Guid Id)
        {
            throw new NotImplementedException();
        }

        public async Task<UserResponseModel> GetUserByIdAsync(Guid Id)
        {
            throw new NotImplementedException();
        }

        public async Task<ProfileResponseModel> GetUserProfileAsync(Guid Id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<UserResponseModel>> GetUsers()
        {
            throw new NotImplementedException();
        }

        public async Task<ProfileResponseModel> GetUserSettingsAsync(Guid Id)
        {
            throw new NotImplementedException();
        }

        public async Task<UserResponseModel> UpdateUserAsync(UpdateUserRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
