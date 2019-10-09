using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Users.Services.Users.Interfaces;
using Users.Services.Models.Requests;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using Users.API.Models.Search.Users;
using Common.Core.Extensions;
using Users.Services.Models.Responses;
using Users.Core.Domain;

namespace Users.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseController
    {
        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService, IMapper mapper) : base(mapper)
        {
            _usersService = usersService;
        }

        [HttpGet]
        public IActionResult GetUsers(UsersSimpleSearchModel searchModel)
        {
            var users = _usersService.GetUsers()
                .ApplySimpleFilter(searchModel.SearchTerm, UsersSearchFilter.SearchableFields);

            var result = GetListResponse<UserResponseModel, User>(searchModel, users);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetUserById(Guid id)
        {
            var user = _usersService.GetUserById(id);

            var result = Mapper.Map<UserResponseModel>(user);

            return Ok(result);
        }

        [HttpGet("{id}/profile")]
        public IActionResult GetUserProfileById(Guid id)
        {
            var profile = _usersService.GetUserProfile(id);

            var result = Mapper.Map<ProfileResponseModel>(profile);

            return Ok(result);
        }

        [HttpGet("{id}/setting")]
        public IActionResult GetUserSettingsById(Guid id)
        {
            var userSetting = _usersService.GetUserSettings(id);

            var result = Mapper.Map<SettingResponseModel>(userSetting);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateUserRequest request)
        {
            var user = await _usersService.UpdateUserAsync(id, request);

            var result = Mapper.Map<UserResponseModel>(user);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _usersService.DeleteUserAsync(id);
            return Ok();
        }
    }
}