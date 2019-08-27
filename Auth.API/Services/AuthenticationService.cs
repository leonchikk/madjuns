using Auth.Core.Entities;
using Authentication.API.Interfaces;
using Authentication.Models.Requests;
using Authentication.Models.Responses;
using Common.Core.Helpers;
using Common.Core.Interfaces;
using System;
using System.Linq;

namespace Authentication.API.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IRepository<Account> _accountsRepository;
        private readonly ITokenService _tokenService;

        public AuthenticationService(IRepository<Account> accountsRepository, ITokenService tokenService)
        {
            _accountsRepository = accountsRepository;
            _tokenService = tokenService;
        }

        public AuthenticationToken Login(AuthenticationRequest request)
        {
            Account account = _accountsRepository.FindBy(x => x.Email == request.Email && x.Password == CryptographyHelper.EncryptString(request.Password)).FirstOrDefault();

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
