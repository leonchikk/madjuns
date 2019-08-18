using Auth.API.Interfaces;
using Auth.API.Models.Requests;
using Authentication.API.Interfaces;
using Authentication.Models.Requests;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Auth.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IAuthenticationService _authenticationService;

        public AuthController(IAccountService accountService, IAuthenticationService authenticationService)
        {
            _accountService = accountService;
            _authenticationService = authenticationService;
        }

        [HttpPost("sign-up")]
        public async Task<IActionResult> SignUp(CreateAccountRequest request)
        {
            await _accountService.CreateUserAsync(request);
            return Ok();
        }

        [HttpPost("sign-in")]
        public IActionResult SignIn(AuthenticationRequest request)
        {
            return Ok(_authenticationService.Login(request));
        }

        [HttpGet("verify-email")]
        public async Task<IActionResult> VerifyEmail([FromQuery] VerifyEmailRequest request)
        {
            await _accountService.VerifyEmailAsync(request);
            return Redirect(request.RedirectUrl);
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordRequest request)
        {
            await _accountService.ForgotPasswordAsync(request);
            return Ok();
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest request)
        {
            await _accountService.ResetPasswordAsync(request);
            return Ok();
        }
    }
}
