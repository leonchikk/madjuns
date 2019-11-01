using AutoMapper;
using Common.Core.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Users.API.Models.Responses;
using Users.API.Models.Search.Subscribers;
using Users.Core.Domain;
using Users.Services.Services.Subscriptions;

namespace Users.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SubscribersController : BaseController
    {
        private readonly ISubscriptionsService _subscriptionsService;

        public SubscribersController(ISubscriptionsService subscriptionsService, IMapper mapper): base(mapper)
        {
            _subscriptionsService = subscriptionsService;
        }

        [HttpGet]
        public IActionResult GetUserSubscribers([FromQuery] SubscribersSimpleSearchModel searchModel)
        {
            var subscribers = _subscriptionsService.GetUserSubscribers(CurrentUserId)
                .ApplySimpleFilter(searchModel.SearchTerm, SubscribersSearchFilter.SearchableFields);

            var result = GetListResponse<BaseUserResponseModel, UserSubscriber>(searchModel, subscribers);

            return Ok(result);
        }

        [HttpPut("subscribe-to/{targetUserId}")]
        public async Task<IActionResult> SendRequestToBeFriendAsync(Guid targetUserId)
        {
            var subscribeTo = await _subscriptionsService.SendRequestToBeFriendAsync(CurrentUserId, targetUserId);

            var result = Mapper.Map<BaseUserResponseModel>(subscribeTo);

            return Ok(result);
        }

        [HttpDelete("reject/{targetUserId}")]
        public async Task<IActionResult> RejectSubscriptionAsync(Guid targetUserId)
        {
            await _subscriptionsService.RejectSubscription(CurrentUserId, targetUserId);
            return Ok();
        }
    }
}