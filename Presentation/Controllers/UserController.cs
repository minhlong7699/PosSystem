using Contract.Service;
using Microsoft.AspNetCore.Mvc;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {

        private readonly IServiceManager _service;

        public UserController(IServiceManager service)
        {
            _service = service;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllUsers([FromQuery]UserParameters userParamters)
        {
            var pagedResult = await _service.UserService.GetAllUsersAsync(userParamters, trackChanges: false);
            Response.Headers.Add("X-Pagination", System.Text.Json.JsonSerializer.Serialize(pagedResult.metaData));
            return Ok(pagedResult.users);
        }


        [HttpGet("{userId:guid}", Name ="GetUser")]

        public async Task<IActionResult> GetUser(Guid userId)
        {
            var user = await _service.UserService.GetUserAsync(userId, trackChanges: false);
            return Ok(user);
        }


        [HttpPost]
        public async Task<IActionResult> CreateUser([FromForm]UserUpdateCreateDto userCreate)
        {
            var user = await _service.UserService.CreateUserAsync(userCreate, trackChanges: false);
            return CreatedAtRoute("GetUser", new {user.UserId}, user);
        }

    }
}
