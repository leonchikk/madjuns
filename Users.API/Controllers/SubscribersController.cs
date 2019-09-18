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
    public class SubscribersController : BaseController
    {
        private readonly IUsersService _usersService;

        public SubscribersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpGet("userId")]
        public IActionResult GetUserSubscribers(Guid userId)
        {
            return Ok(_usersService.GetUserSubscribers(userId));
        }

        [HttpPut("{currentUserId}/send-request-to-be-friend/{targetUserId}")]
        public async Task<IActionResult> SendRequestToBeFriend(Guid currentUserId, Guid targetUserId)
        {
            return Ok(await _usersService.SendRequestToBeFriendAsync(currentUserId, targetUserId));
        }
    }
}