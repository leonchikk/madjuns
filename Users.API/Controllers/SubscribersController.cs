using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
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

        [HttpDelete("{currentUserId}/from/{targetUserId}")]
        public async Task<IActionResult> RejectSubscription(Guid currentUserId, Guid targetUserId)
        {
            await _usersService.RejectSubscription(currentUserId, targetUserId);
            return Ok();
        }
    }
}