using Communication.API.Application.Commands.Channels;
using Communication.API.Models.RequestModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Communication.API.Controllers
{
    [Route("api/[controller]")]
    public class ChannelsController : Controller
    {
        private readonly IMediator _mediator;

        public ChannelsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateChannelAsync([FromBody] CreateChannelRequestModel request)
        {
            var createChannelCommand = new CreateChannelCommand(request.Name, request.LogoUrl, request.Visibility);

            var result = await _mediator.Send(createChannelCommand);

            return Ok(result);
        }
    }
}
