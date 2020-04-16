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
    public class SubscribersController : ControllerBase
    {
        private readonly IHttpUsersClient _httpUsersClient;

        public SubscribersController(IHttpUsersClient httpUsersClient)
        {
            _httpUsersClient = httpUsersClient;
        }

        [HttpGet]
        public async Task<IActionResult> GetUserSubscribers([FromQuery] SubscribersSimpleSearchModel searchModel)
        {
            return Ok(await _httpUsersClient.GetUserSubscribersAsync(searchModel));
        }

        [HttpPut("subscribe-to/{targetUserId}")]
        public async Task<IActionResult> SendRequestToBeFriendAsync(Guid targetUserId)
        {
            return Ok(await _httpUsersClient.SendRequestToBeFriendAsync(targetUserId));
        }

        [HttpDelete("reject/{targetUserId}")]
        public async Task<IActionResult> RejectSubscriptionAsync(Guid targetUserId)
        {
            await _httpUsersClient.RejectSubscriptionAsync(targetUserId);
            return Ok();
        }
    }
}