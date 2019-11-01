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
    public class UsersController : ControllerBase
    {
        private readonly IHttpUsersClient _httpUsersClient;

        public UsersController(IHttpUsersClient httpUsersClient)
        {
            _httpUsersClient = httpUsersClient;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsersAsync([FromQuery]UsersSimpleSearchModel searchModel)
        {
            return Ok(await _httpUsersClient.GetUsersAsync(searchModel));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserByIdAsync(Guid id)
        {
            return Ok(await _httpUsersClient.GetUserByIdAsync(id));
        }

        [HttpGet("{id}/profile")]
        public async Task<IActionResult> GetUserProfileByIdAsync(Guid id)
        {
            return Ok(await _httpUsersClient.GetUserProfileByIdAsync(id));
        }

        [HttpGet("{id}/setting")]
        public async Task<IActionResult> GetUserSettingsByIdAsync(Guid id)
        {
            return Ok(await _httpUsersClient.GetUserSettingsByIdAsync(id));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] UpdateUserRequest request)
        {
            return Ok(await _httpUsersClient.UpdateUserAsync(id, request));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            await _httpUsersClient.DeleteUserAsync(id);
            return Ok();
        }
    }
}