using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiGateway.Web.HttpClients.Interfaces;
using ApiGateway.Web.HttpClients.Models.UsersAPI.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ApiGateway.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IHttpUsersClient _httpUsersClient;
        private readonly IConfiguration _configuration;

        public UsersController(IHttpUsersClient httpUsersClient, IConfiguration configuration)
        {
            _httpUsersClient = httpUsersClient;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers([FromQuery]UsersSimpleSearchModel searchModel)
        {
            var serviceUrl = _configuration.GetSection("ApiUrls:UsersApi").Value;
            return Ok(await _httpUsersClient.GetUsers(serviceUrl, searchModel));
        }
    }
}