using Contract.Service;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> GetAllUsers([FromQuery]UserParamters userParamters)
        {
            var pagedResult = await _service.UserService.GetAllUsersAsync(userParamters, trackChanges: false);
            Response.Headers.Add("X-Pagination", System.Text.Json.JsonSerializer.Serialize(pagedResult.metaData));
            return Ok(pagedResult.users);
        }


        [HttpGet("{userId:guid}")]

        public async Task<IActionResult> GetUser(Guid userId)
        {
            var user = await _service.UserService.GetUserAsync(userId, trackChanges: false);
            return Ok(user);
        }

    }
}
