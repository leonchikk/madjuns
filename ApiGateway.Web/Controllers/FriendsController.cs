using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiGateway.Web.HttpClients.Interfaces;
using ApiGateway.Web.HttpClients.Models.UsersAPI.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiGateway.Web.Controllers
{
    [Authorize]
    [Area("api/users-api")]
    [Route("[area]/[controller]")]
    [ApiController]
    public class FriendsController : ControllerBase
    {
        private readonly IHttpUsersClient _httpUsersClient;

        public FriendsController(IHttpUsersClient httpUsersClient)
        {
            _httpUsersClient = httpUsersClient;
        }

        [HttpGet]
        public async Task<IActionResult> GetUserFriends([FromQuery] FriendsSimpleSearchModel searchModel)
        {
            return Ok(await _httpUsersClient.GetUserFriendsAsync(searchModel));
        }

        [HttpPut("add/{subscriberId}")]
        public async Task<IActionResult> AddToFriendAsync(Guid subscriberId)
        {
            return Ok(await _httpUsersClient.AddToFriendAsync(subscriberId));
        }

        [HttpDelete("{friendId}")]
        public async Task<IActionResult> DeleteAsync(Guid friendId)
        {
            await _httpUsersClient.RemoveFriendAsync(friendId);
            return Ok();
        }
    }
}