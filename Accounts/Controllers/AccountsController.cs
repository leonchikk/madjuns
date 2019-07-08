using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accounts.Interfaces;
using Accounts.Models.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Accounts.Controllers
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
