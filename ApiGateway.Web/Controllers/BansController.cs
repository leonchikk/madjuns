using ApiGateway.Web.HttpClients.Interfaces;
using ApiGateway.Web.HttpClients.Models.UsersAPI.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace ApiGateway.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BansController : ControllerBase
    {
        private readonly IHttpUsersClient _httpUsersClient;
        private readonly IConfiguration _configuration;

        public BansController(IHttpUsersClient httpUsersClient, IConfiguration configuration)
        {
            _httpUsersClient = httpUsersClient;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> GetUserBlackList([FromQuery] BansSimpleSearchModel searchModel)
        {
            var serviceUrl = _configuration.GetSection("ApiUrls:UsersApi").Value;
            return Ok(await _httpUsersClient.GetUserBlackList(serviceUrl, searchModel));
        }
    }
}