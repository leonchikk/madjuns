using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Users.Services.Models.Responses;
using Users.Services.Services.Bans;

namespace Users.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BansController : BaseController
    {
        private readonly IBansService _bansService;

        public BansController(IBansService bansService, IMapper mapper): base(mapper)
        {
            _bansService = bansService;
        }

        [HttpGet]
        public IActionResult GetUserBlackList()
        {
            var blackList = _bansService.GetUserBlackList(CurrentUserId);

            return Ok(Mapper.Map<IEnumerable<BaseUserResponseModel>>(blackList));
        }

        [HttpPut("add/{targetUserId}")]
        public async Task<IActionResult> AddUserToBlackList(Guid targetUserId)
        {
            var bannedUser = await _bansService.AddToBlackListAsync(CurrentUserId, targetUserId);
           
            return Ok(Mapper.Map<BaseUserResponseModel>(bannedUser));
        }

        [HttpDelete("{bannedUserId}")]
        public async Task<IActionResult> RemoveFromBlackList(Guid bannedUserId)
        {
            await _bansService.RemoveFromBlackList(CurrentUserId, bannedUserId);

            return Ok();
        }
    }
}