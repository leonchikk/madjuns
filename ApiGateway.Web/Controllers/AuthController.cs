using ApiGateway.Web.HttpClients.Interfaces;
using ApiGateway.Web.HttpClients.Models.Auth;
using ApiGateway.Web.HttpClients.Models.Auth.ForgotPassword;
using ApiGateway.Web.HttpClients.Models.Auth.ResetPassword;
using ApiGateway.Web.HttpClients.Models.Auth.SignUp;
using ApiGateway.Web.HttpClients.Models.Auth.VerifyEmail;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;


namespace ApiGateway.Web.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly IHttpAuthClient _httpAuthClient;
        private readonly IConfiguration _configuration;

        public AuthController(IHttpAuthClient httpAuthClient, IConfiguration configuration)
        {
            _httpAuthClient = httpAuthClient;
            _configuration = configuration;
        }

        [HttpPost("sign-in")]
        public async Task<IActionResult> SignInAsync([FromBody] SignInRequestModel model)
        {
            return Ok(await _httpAuthClient.SignInAsync(model));
        }

        [HttpPost("sign-up")]
        public async Task<IActionResult> SignUpAsync([FromBody] SignUpRequestModel model)
        {
            await _httpAuthClient.SignUpAsync(model);
            return Ok();
        }

        [HttpGet("verify-email")]
        public async Task<IActionResult> VerifyEmailAsync([FromQuery] VerifyEmailRequestModel model)
        {
            var serviceUrl = _configuration.GetSection("ApiUrls:AuthApi").Value;
            var result = await _httpAuthClient.VerifyEmailAsync(serviceUrl, model);

            return Redirect(result.RedirectUrl);
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPasswordAsync(ForgotPasswordRequestModel model)
        {
            await _httpAuthClient.ForgotPasswordAsync(model);
            return Ok();
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPasswordAsync(ResetPasswordRequestModel request)
        {
            await _httpAuthClient.ResetPasswordAsync(request);
            return Ok();
        }
    }
}
