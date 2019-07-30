using Auth.Interfaces;
using Auth.Models.Requests;
using Authentication.Interfaces;
using Authentication.Models.Requests;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Auth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IAuthenticationService _authenticationService;
        private readonly ITokenService _tokenService;

        public AuthController(IAccountService accountService, IAuthenticationService authenticationService,
            ITokenService tokenService)
        {
            _accountService = accountService;
            _authenticationService = authenticationService;
            _tokenService = tokenService;
        }

        [HttpPost("sign-up")]
        public async Task<IActionResult> SignUp(CreateAccountRequest request)
        {
            var account = await _accountService.CreateUserAsync(request);
            return Ok(_tokenService.CreateToken(account));
        }

        [HttpPost("sign-in")]
        public IActionResult SignIn(AuthenticationRequest request)
        {
            return Ok(_authenticationService.Login(request));
        }
    }
}
