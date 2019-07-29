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

        public AuthController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("sign-up")]
        public async Task<IActionResult> SignUp(CreateUserRequest request)
        {
            await _accountService.CreateUserAsync(request);
            return Ok();
        }

        [HttpPost("sign-in")]
        public IActionResult SignIn(AuthenticationRequest request)
        {
            return Ok(_authenticationService.Login(request));
        }
    }
}
