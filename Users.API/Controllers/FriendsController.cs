using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Users.Services.Services.Friends;
using Users.Services.Users.Interfaces;

namespace Users.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FriendsController : BaseController
    {
        private readonly IFriendsService _friendsService;

        public FriendsController(IFriendsService friendsService)
        {
            _friendsService = friendsService;
        }

        [HttpGet("userId")]
        public IActionResult GetUserFriends(Guid userId)
        {
            return Ok(_friendsService.GetUserFriends(userId));
        }

        [HttpPut("{currentUserId}/add-to-friend/{subscriberId}")]
        public async Task<IActionResult> AddToFriend(Guid currentUserId, Guid subscriberId)
        {
            return Ok(await _friendsService.AddToFriendAsync(currentUserId, subscriberId));
        }

        [HttpDelete("{friendId}/from/{currentUserId}")]
        public async Task<IActionResult> Delete(Guid friendId, Guid currentUserId)
        {
            await _friendsService.RemoveFriendAsync(currentUserId, friendId);
            return Ok();
        }
    }
}