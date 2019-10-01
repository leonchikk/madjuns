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

        [HttpGet("userId/get-blacklist")]
        public IActionResult GetUserBlackList(Guid userId)
        {
            return Ok(_bansService.GetUserBlackList(userId));
        }

        [HttpPut("{currentUserId}/add-to-black-list/{targetUserId}")]
        public async Task<IActionResult> AddUserToBlackList(Guid currentUserId, Guid targetUserId)
        {
            return Ok(await _bansService.AddToBlackListAsync(currentUserId, targetUserId));
        }

        [HttpDelete("{bannedUserId}/from/{currentUserId}")]
        public async Task<IActionResult> RemoveFromBlackList(Guid currentUserId, Guid bannedUserId)
        {
            await _bansService.RemoveFromBlackList(currentUserId, bannedUserId);
            return Ok();
        }
    }
}