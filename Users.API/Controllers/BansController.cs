using AutoMapper;
using Common.Core.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Users.API.Models.Search.Bans;
using Users.Core.Domain;
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
        public IActionResult GetUserBlackList([FromQuery] BansSimpleSearchModel searchModel)
        {
            var blackList = _bansService.GetUserBlackList(CurrentUserId)
              .ApplySimpleFilter(searchModel.SearchTerm, BansSearchFilter.SearchableFields);

            var result = GetListResponse<BaseUserResponseModel, BlockedUser>(searchModel, blackList);

            return Ok(result);
        }

        [HttpPut("add/{targetUserId}")]
        public async Task<IActionResult> AddUserToBlackList(Guid targetUserId)
        {
            var bannedUser = await _bansService.AddToBlackListAsync(CurrentUserId, targetUserId);

            var result = Mapper.Map<BaseUserResponseModel>(bannedUser);

            return Ok(result);
        }

        [HttpDelete("{bannedUserId}")]
        public async Task<IActionResult> RemoveFromBlackList(Guid bannedUserId)
        {
            await _bansService.RemoveFromBlackList(CurrentUserId, bannedUserId);

            return Ok();
        }
    }
}