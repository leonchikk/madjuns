using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Users.Services.Services.Subscriptions;

namespace Users.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SubscribersController : BaseController
    {
        private readonly ISubscriptionsService _subscriptionsService;

        public SubscribersController(ISubscriptionsService subscriptionsService)
        {
            _subscriptionsService = subscriptionsService;
        }

        [HttpGet]
        public IActionResult GetUserSubscribers()
        {
            return Ok(_subscriptionsService.GetUserSubscribers(CurrentUserId));
        }

        [HttpPut("subscribe-to/{targetUserId}")]
        public async Task<IActionResult> SendRequestToBeFriend(Guid targetUserId)
        {
            return Ok(await _subscriptionsService.SendRequestToBeFriendAsync(CurrentUserId, targetUserId));
        }

        [HttpDelete("reject/{targetUserId}")]
        public async Task<IActionResult> RejectSubscription(Guid targetUserId)
        {
            await _subscriptionsService.RejectSubscription(CurrentUserId, targetUserId);
            return Ok();
        }
    }
}