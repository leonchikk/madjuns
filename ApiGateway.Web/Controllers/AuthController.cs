using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiGateway.Web.HttpClients.Interfaces;
using ApiGateway.Web.HttpClients.Models.Auth;
using Microsoft.AspNetCore.Mvc;


namespace ApiGateway.Web.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly IHttpAuthClient _httpAuthClient;

        public AuthController(IHttpAuthClient httpAuthClient)
        {
            _httpAuthClient = httpAuthClient;
        }

        [HttpPost("sign-in")]
        public async Task<IActionResult> SignIn([FromBody] AuthRequestModel model)
        {
            return Ok(await _httpAuthClient.AuthAsync(model));
        }
    }
}
