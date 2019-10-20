using Auth.API.Interfaces;
using Auth.API.Models.Requests;
using Auth.API.Models.Responses;
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
        public async Task<IActionResult> SignUpAsync(CreateAccountRequest request)
        {
            await _accountService.CreateUserAsync(request);
            return Ok();
        }

        [HttpPost("sign-in")]
        public IActionResult SignInAsync(AuthenticationRequest request)
        {
            return Ok(_authenticationService.Login(request));
        }

        [HttpGet("verify-email")]
        public async Task<IActionResult> VerifyEmailAsync([FromQuery] VerifyEmailRequest request)
        {
            await _accountService.VerifyEmailAsync(request);

            var response = new VerifyEmailResponseModel()
            {
                RedirectUrl = request.RedirectUrl
            };

            return Ok(response);
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPasswordAsync(ForgotPasswordRequest request)
        {
            await _accountService.ForgotPasswordAsync(request);
            return Ok();
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPasswordAsync(ResetPasswordRequest request)
        {
            await _accountService.ResetPasswordAsync(request);
            return Ok();
        }
    }
}
