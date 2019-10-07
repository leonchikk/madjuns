using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Users.Services.Services.Friends;
using Users.Services.Users.Interfaces;

namespace Users.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class FriendsController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IFriendsService _friendsService;

        public FriendsController(IFriendsService friendsService, IMapper mapper): base(mapper)
        {
            _mapper = mapper;
            _friendsService = friendsService;
        }

        [HttpGet]
        public IActionResult GetUserFriends()
        {
            return Ok(_friendsService.GetUserFriends(CurrentUserId));
        }

        [HttpPut("add/{subscriberId}")]
        public async Task<IActionResult> AddToFriend( Guid subscriberId)
        {
            return Ok(await _friendsService.AddToFriendAsync(CurrentUserId, subscriberId));
        }

        [HttpDelete("{friendId}")]
        public async Task<IActionResult> Delete(Guid friendId)
        {
            await _friendsService.RemoveFriendAsync(CurrentUserId, friendId);
            return Ok();
        }
    }
}