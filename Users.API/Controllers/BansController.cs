using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Users.API.Interfaces;

namespace Users.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BansController : BaseController
    {
        private readonly IUsersService _usersService;

        public BansController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpGet("userId/get-blacklist")]
        public IActionResult GetUserBlackList(Guid userId)
        {
            return Ok(_usersService.GetUserBlackList(userId));
        }

        [HttpPut("{currentUserId}/add-to-black-list/{targetUserId}")]
        public async Task<IActionResult> AddUserToBlackList(Guid currentUserId, Guid targetUserId)
        {
            return Ok(await _usersService.AddToBlackListAsync(currentUserId, targetUserId));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            //await _usersService.DeleteUserAsync(id);
            return Ok();
        }
    }
}