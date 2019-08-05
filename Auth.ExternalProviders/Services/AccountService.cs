using Auth.Data.Entities;
using Auth.ExternalProviders.Interfaces.Internal;
using Auth.ExternalProviders.Interfaces.Public;
using Auth.ExternalProviders.Models;
using Common.Core.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.ExternalProviders.Services
{
    internal class AccountService : Interfaces.Public.IExternalAuthProvider
    {
        private readonly IRepository<Account> _accountRepository;
        private readonly ITokenService _tokenService;
        private readonly IVkAuthProvider _vkAuthProvider;
        private readonly IGoogleAuthProvider _googleAuthProvider;
        private readonly IFacebookAuthProvider _facebookAuthProvider;


        public AccountService(IRepository<Account> accountRepository,
            ITokenService tokenService,
            IVkAuthProvider vkAuthProvider,
            IGoogleAuthProvider googleAuthProvider,
            IFacebookAuthProvider facebookAuthProvider)
        {
            _accountRepository = accountRepository;
            _vkAuthProvider = vkAuthProvider;
            _googleAuthProvider = googleAuthProvider;
            _facebookAuthProvider = facebookAuthProvider;
            _tokenService = tokenService;
        }

        public async Task<AuthorizationToken> FacebookLoginAsync(ProviderToken providerResource)
        {
            var facebookUser = await _facebookAuthProvider.GetAccountInfoAsync(providerResource.Token);
            return await CreateTokenAsync(facebookUser);
        }

        public async Task<AuthorizationToken> GoogleLoginAsync(ProviderToken providerResource)
        {
            var googleUser = await _googleAuthProvider.GetAccountInfoAsync(providerResource.Token);
            return await CreateTokenAsync(googleUser);
        }

        public async Task<AuthorizationToken> VkLoginAsync(ProviderToken providerResource)
        {
            var vkUser = await _vkAuthProvider.GetAccountInfoAsync(providerResource.Token);
            return await CreateTokenAsync(vkUser);
        }

        private async Task<AuthorizationToken> CreateTokenAsync(ProviderUser providerUser)
        {
            var domainUser =  _accountRepository.FindBy(a => a.ProviderId == providerUser.ProviderId).FirstOrDefault();

            if (domainUser == null)
            {
                domainUser = await _accountRepository.AddAsync(domainUser);
            }

            return _tokenService.CreateToken(domainUser);
        }
    }
}
