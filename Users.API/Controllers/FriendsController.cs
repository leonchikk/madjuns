using AutoMapper;
using Common.Core.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Users.API.Models.Search.Friends;
using Users.Core.Domain;
using Users.Services.Models.Responses;
using Users.Services.Services.Friends;

namespace Users.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class FriendsController : BaseController
    {
        private readonly IFriendsService _friendsService;

        public FriendsController(IFriendsService friendsService, IMapper mapper): base(mapper)
        {
            _friendsService = friendsService;
        }

        [HttpGet]
        public IActionResult GetUserFriends([FromQuery] FriendsSimpleSearchModel searchModel)
        {
            var friends = _friendsService.GetUserFriends(CurrentUserId)
                .ApplySimpleFilter(searchModel.SearchTerm, FriendsSearchFilter.SearchableFields);

            var result = GetListResponse<BaseUserResponseModel, UserFriend>(searchModel, friends);

            return Ok(result);
        }

        [HttpPut("add/{subscriberId}")]
        public async Task<IActionResult> AddToFriend( Guid subscriberId)
        {
            var friend = await _friendsService.AddToFriendAsync(CurrentUserId, subscriberId);

            var result = Mapper.Map<BaseUserResponseModel>(friend);

            return Ok(result);
        }

        [HttpDelete("{friendId}")]
        public async Task<IActionResult> Delete(Guid friendId)
        {
            await _friendsService.RemoveFriendAsync(CurrentUserId, friendId);
            return Ok();
        }
    }
}