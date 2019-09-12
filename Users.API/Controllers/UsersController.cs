using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Users.API.Interfaces;
using Users.API.Models.Requests;

namespace Users.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            return Ok(_usersService.GetUsers());
        }

        [HttpGet("userId/get-friends")]
        public IActionResult GetUserFriends(Guid userId)
        {
            return Ok(_usersService.GetUserFriends(userId));
        }

        [HttpGet("userId/get-subscribers")]
        public IActionResult GetUserSubscribers(Guid userId)
        {
            return Ok(_usersService.GetUserSubscribers(userId));
        }

        [HttpGet("userId/get-blacklist")]
        public IActionResult GetUserBlackList(Guid userId)
        {
            return Ok(_usersService.GetUserBlackList(userId));
        }

        [HttpGet("{id}")]
        public IActionResult GetUserById(Guid id)
        {
            return Ok(_usersService.GetUserById(id));
        }

        [HttpGet("{id}/profile")]
        public IActionResult GetUserProfileById(Guid id)
        {
            return Ok(_usersService.GetUserProfile(id));
        }

        [HttpGet("{id}/setting")]
        public IActionResult GetUserSettingsById(Guid id)
        {
            return Ok(_usersService.GetUserSettings(id));
        }

        [HttpPut("{currentUserId}/add-to-friend/{subscriberId}")]
        public async Task<IActionResult> AddToFriend(Guid currentUserId, Guid subscriberId)
        {
            return Ok(await _usersService.AddToFriendAsync(currentUserId, subscriberId));
        }

        [HttpPut("{currentUserId}/send-request-to-be-friend/{targetUserId}")]
        public async Task<IActionResult> SendRequestToBeFriend(Guid currentUserId, Guid targetUserId)
        {
            return Ok(await _usersService.SendRequestToBeFriendAsync(currentUserId, targetUserId));
        }

        [HttpPut("{currentUserId}/add-to-black-list/{targetUserId}")]
        public async Task<IActionResult> AddUserToBlackList(Guid currentUserId, Guid targetUserId)
        {
            return Ok(await _usersService.AddToBlackListAsync(currentUserId, targetUserId));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateUserRequest request)
        {
            return Ok(await _usersService.UpdateUserAsync(id, request));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _usersService.DeleteUserAsync(id);
            return Ok();
        }
    }
}