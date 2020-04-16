using ApiGateway.Web.HttpClients.Interfaces;
using ApiGateway.Web.HttpClients.Models.CommunicationAPI.RequestModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;


namespace ApiGateway.Web.Controllers.CommunicationService
{
    [Route("api/[controller]")]
    public class ChannelsController : Controller
    {
        private readonly IHttpCommunicationClient _httpClient;

        public ChannelsController(IHttpCommunicationClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateChannelAsync([FromBody] CreateChannelRequest request)
        {
            var result = await _httpClient.CreateChannelAsync(request);

            return Ok(result);
        }
    }
}
