using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;

namespace Users.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        public Guid CurrentUserId
        {
            get
            {
                var indentity = HttpContext.User.Identity as ClaimsIdentity;
                return Guid.Parse(indentity.FindFirst("AccountId").Value);
            }
        }
    }
}