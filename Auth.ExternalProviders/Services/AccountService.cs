using Auth.Core.Entities;
using Auth.ExternalProviders.Interfaces.Internal;
using Auth.ExternalProviders.Models;
using Common.Core.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.ExternalProviders.Services
{
    internal class AccountService : Interfaces.Public.IExternalAuthProvider
    {
        private readonly IRepository<Account> _accountsRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenService _tokenService;
        private readonly IVkAuthProvider _vkAuthProvider;
        private readonly IGoogleAuthProvider _googleAuthProvider;
        private readonly IFacebookAuthProvider _facebookAuthProvider;


        public AccountService(
            IRepository<Account> accountsRepository,
            IUnitOfWork unitOfWork,
            ITokenService tokenService,
            IVkAuthProvider vkAuthProvider,
            IGoogleAuthProvider googleAuthProvider,
            IFacebookAuthProvider facebookAuthProvider)
        {
            _accountsRepository = accountsRepository;
            _unitOfWork = unitOfWork;
            _vkAuthProvider = vkAuthProvider;
            _googleAuthProvider = googleAuthProvider;
            _facebookAuthProvider = facebookAuthProvider;
            _tokenService = tokenService;
        }

        public async Task<AuthorizationToken> FacebookLoginAsync(ProviderToken providerResource)
        {
            ProviderUser facebookUser = await _facebookAuthProvider.GetAccountInfoAsync(providerResource.Token);
            return await CreateTokenAsync(facebookUser);
        }

        public async Task<AuthorizationToken> GoogleLoginAsync(ProviderToken providerResource)
        {
            ProviderUser googleUser = await _googleAuthProvider.GetAccountInfoAsync(providerResource.Token);
            return await CreateTokenAsync(googleUser);
        }

        public async Task<AuthorizationToken> VkLoginAsync(ProviderToken providerResource)
        {
            ProviderUser vkUser = await _vkAuthProvider.GetAccountInfoAsync(providerResource.Token);
            return await CreateTokenAsync(vkUser);
        }

        private async Task<AuthorizationToken> CreateTokenAsync(ProviderUser providerUser)
        {
            Account domainUser = _accountsRepository.FindBy(a => a.ProviderId == providerUser.ProviderId).FirstOrDefault();

            if (domainUser == null)
            {
                domainUser = await _accountsRepository.AddAsync(domainUser);
                await _unitOfWork.SaveChangesAsync();
            }

            return _tokenService.CreateToken(domainUser);
        }
    }
}
