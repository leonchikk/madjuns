using Auth.Interfaces;
using Auth.Models.Requests;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Auth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountsController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("create-user")]
        public async Task<IActionResult> CreateUser(CreateUserRequest request)
        {
            await _accountService.CreateUserAsync(request);
            return Ok();
        }
    }
}
