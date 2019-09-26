using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Users.Services.Services.Subscriptions;
using Users.Services.Users.Interfaces;

namespace Users.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscribersController : BaseController
    {
        private readonly ISubscriptionsService _subscriptionsService;

        public SubscribersController(ISubscriptionsService subscriptionsService)
        {
            _subscriptionsService = subscriptionsService;
        }

        [HttpGet("userId")]
        public IActionResult GetUserSubscribers(Guid userId)
        {
            return Ok(_subscriptionsService.GetUserSubscribers(userId));
        }

        [HttpPut("{currentUserId}/send-request-to-be-friend/{targetUserId}")]
        public async Task<IActionResult> SendRequestToBeFriend(Guid currentUserId, Guid targetUserId)
        {
            return Ok(await _subscriptionsService.SendRequestToBeFriendAsync(currentUserId, targetUserId));
        }

        [HttpDelete("{currentUserId}/from/{targetUserId}")]
        public async Task<IActionResult> RejectSubscription(Guid currentUserId, Guid targetUserId)
        {
            await _subscriptionsService.RejectSubscription(currentUserId, targetUserId);
            return Ok();
        }
    }
}