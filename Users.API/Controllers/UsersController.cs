using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Users.Services.Users.Interfaces;
using Users.Services.Models.Requests;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;

namespace Users.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseController
    {
        private readonly IMapper _mapper;

        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService, IMapper mapper) : base(mapper)
        {
            _mapper = mapper;
            _usersService = usersService;
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            return Ok(_usersService.GetUsers());
        }

        [HttpGet("{id}")]
        public IActionResult GetUserById(Guid id)
        {
            return Ok(_usersService.GetUserById(id));
        }

        [HttpGet("{id}/profile")]
        public IActionResult GetUserProfileById(Guid id)
        {
            return Ok(_usersService.GetUserProfile(id));
        }

        [HttpGet("{id}/setting")]
        public IActionResult GetUserSettingsById(Guid id)
        {
            return Ok(_usersService.GetUserSettings(id));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateUserRequest request)
        {
            return Ok(await _usersService.UpdateUserAsync(id, request));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _usersService.DeleteUserAsync(id);
            return Ok();
        }
    }
}