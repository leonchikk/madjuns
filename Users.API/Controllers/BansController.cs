using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Users.Services.Services.Bans;
using Users.Services.Users.Interfaces;

namespace Users.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BansController : BaseController
    {
        private readonly IBansService _bansService;

        public BansController(IBansService bansService)
        {
            _bansService = bansService;
        }

        [HttpGet]
        public IActionResult GetUserBlackList()
        {
            return Ok(_bansService.GetUserBlackList(CurrentUserId));
        }

        [HttpPut("add/{targetUserId}")]
        public async Task<IActionResult> AddUserToBlackList(Guid targetUserId)
        {
            return Ok(await _bansService.AddToBlackListAsync(CurrentUserId, targetUserId));
        }

        [HttpDelete("{bannedUserId}")]
        public async Task<IActionResult> RemoveFromBlackList(Guid bannedUserId)
        {
            await _bansService.RemoveFromBlackList(CurrentUserId, bannedUserId);
            return Ok();
        }
    }
}