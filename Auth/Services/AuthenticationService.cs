﻿using Auth.Core.Interfaces;
using Authentication.Interfaces;
using Authentication.Models.Requests;
using Authentication.Models.Responses;
using Common.Core.Helpers;
using System;
using System.Linq;

namespace Authentication.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenService _tokenService;

        public AuthenticationService(IUnitOfWork unitOfWork, ITokenService tokenService)
        {
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
        }

        public AuthenticationToken Login(AuthenticationRequest request)
        {
            var account = _unitOfWork.AccountsRepository.FindBy(x => x.Email == request.Email && x.Password == CryptographyHelper.EncryptString(request.Password)).FirstOrDefault();

            if (account == null)
            {
                throw new Exception("Incorrect email or password!");
            }

            if (!account.IsEmailVerified)
            {
                throw new Exception("Account has not verified yet!");
            }

            return _tokenService.CreateToken(account);
        }
    }
}
