using Contract.Service;
using Microsoft.AspNetCore.Mvc;
using Shared.DataTransferObjects.Authentication;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/authentication")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IServiceManager _service;

        public AuthenticationController(IServiceManager service) {
            _service = service;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Authenticate([FromBody] UserForAuthenticationDto user)
        {
            if(!await _service.AuthenticationService.ValidateUser(user)) return Unauthorized();
            return Ok(new {token = await _service.AuthenticationService.CreateToken()});
        }
    }
}
