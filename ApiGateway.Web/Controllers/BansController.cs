using ApiGateway.Web.HttpClients.Interfaces;
using ApiGateway.Web.HttpClients.Models.UsersAPI.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace ApiGateway.Web.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BansController : ControllerBase
    {
        private readonly IHttpUsersClient _httpUsersClient;

        public BansController(IHttpUsersClient httpUsersClient)
        {
            _httpUsersClient = httpUsersClient;
        }

        [HttpGet]
        public async Task<IActionResult> GetUserBlackListAsync([FromQuery] BansSimpleSearchModel searchModel)
        {
            return Ok(await _httpUsersClient.GetUserBlackListAsync(searchModel));
        }

        [HttpPut("add/{targetUserId}")]
        public async Task<IActionResult> AddUserToBlackListAsync(Guid targetUserId)
        {
            return Ok(await _httpUsersClient.AddUserToBlackListAsync(targetUserId));
        }

        [HttpDelete("{bannedUserId}")]
        public async Task<IActionResult> RemoveFromBlackListAsync(Guid bannedUserId)
        {
            await _httpUsersClient.RemoveFromBlackListAsync(bannedUserId);
            return Ok();
        }
    }
}