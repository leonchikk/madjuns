using Authentication.Data.Entities;
using Authentication.Data.Interfaces;
using Authentication.Interfaces;
using Authentication.Models.Requests;
using Authentication.Models.Responses;
using Common.Core.Events;
using Common.Core.Helpers;
using EasyNetQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenService _tokenService;

        public AuthenticationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public AuthenticationToken Login(AuthenticationRequest request)
        {
            var user = _unitOfWork.UsersRepository.FindBy(x => x.Email == request.Email && x.Password == CryptographyHelper.EncryptString(request.Password)).FirstOrDefault();

            if (user == null)
                throw new Exception("Incorrect email or password!");

            return _tokenService.CreateToken(user);
        }
    }
}
