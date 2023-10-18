using Contract.Service;
using Contract.Service.EmailServices;
using Entity.Models.Email;
using Microsoft.AspNetCore.Mvc;
using Shared.DataTransferObjects.Authentication;

namespace Presentation.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IServiceManager _service;
        private readonly IEmailService _emailServices;
        public AuthenticationController(IServiceManager service, IEmailService emailServices)
        {
            _service = service;
            _emailServices = emailServices;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDto userForRegistration)
        {
            var result = await _service.AuthenticationService.RegisterUser(userForRegistration);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                return BadRequest(ModelState);
            }
            return StatusCode(201);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Authenticate([FromBody] UserForAuthenticationDto user)
        {
            if (!await _service.AuthenticationService.ValidateUser(user))
                return Unauthorized();
            return Ok(new
            {
                Token = await _service
            .AuthenticationService.CreateToken()
            });
        }

        [HttpGet("testmail")]
        public IActionResult TestEmail()
        {
            var mess = new Message(new string[] { "minhlong769999@gmail.com" }, "Test", "Testing");
            _emailServices.SendEmail(mess);
            return Ok();
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            var result = await _service.AuthenticationService.ForgotPasswordAsync(email);
            if(result.IsSuccess) return Ok(result);
            return BadRequest(result);
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPassworDto resetPassworDto)
        {
            var result = await _service.AuthenticationService.ResetUserPasswordAsync(resetPassworDto);
            if(result.IsSuccess) return Ok(result);
            return BadRequest();
        }
    }
}
