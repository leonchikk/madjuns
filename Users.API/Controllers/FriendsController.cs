using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Users.API.Interfaces;

namespace Users.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FriendsController : BaseController
    {
        private readonly IUsersService _usersService;

        public FriendsController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpGet("userId")]
        public IActionResult GetUserFriends(Guid userId)
        {
            return Ok(_usersService.GetUserFriends(userId));
        }

        [HttpPut("{currentUserId}/add-to-friend/{subscriberId}")]
        public async Task<IActionResult> AddToFriend(Guid currentUserId, Guid subscriberId)
        {
            return Ok(await _usersService.AddToFriendAsync(currentUserId, subscriberId));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            //await _usersService.RemoveFriendAsync(id);
            return Ok();
        }
    }
}