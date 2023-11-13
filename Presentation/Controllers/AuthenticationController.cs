using Contract.Service;
using Contract.Service.EmailServices;
using Entity.Models.Email;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.DataTransferObjects.Authentication;

namespace Presentation.Controllers
{
    /// <summary>
    /// API for user authentication and registration.
    /// </summary>
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


        /// <summary>
        /// Register a new user.
        /// </summary>
        /// <param name="userForRegistration">User registration data.</param>
        /// <returns>
        /// Returns status code 201 (Created) if the registration is successful.
        /// Returns status code 400 (Bad Request) if registration fails with model validation errors.
        /// </returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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

        /// <summary>
        /// Authenticate a user to obtain an access token. for testing "userName": "admintest", "password": "admin123456789
        /// </summary>
        /// <param name="user">User authentication data. for testing "userName": "admintest", "password": "admin123456789"
        ///</param>
        /// <returns>
        /// Returns an access token if authentication is successful.
        /// Returns status code 401 (Unauthorized) if authentication fails.
        /// </returns>
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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


        /// <summary>
        /// Test sending an email.
        /// </summary>
        /// <returns>
        /// Returns status code 200 (OK) if the test email is sent successfully.
        /// </returns>
        [HttpPost("testmail")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult TestEmail(string[] receiver, string subject, string context)
        {
            var mess = new Message(receiver, subject, context);
            _emailServices.SendEmail(mess);
            return Ok();
        }


        /// <summary>
        /// Initiate the process for resetting a forgotten password.
        /// </summary>
        /// <param name="email">User's email for password reset.</param>
        /// <returns>
        /// Returns status code 200 (OK) if the password reset initiation is successful.
        /// Returns status code 400 (Bad Request) if initiation fails.
        /// </returns>
        [HttpPost("forgot-password")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            var result = await _service.AuthenticationService.ForgotPasswordAsync(email);
            if(result.IsSuccess) return Ok(result);
            return BadRequest(result);
        }

        /// <summary>
        /// Reset a user's password.
        /// </summary>
        /// <param name="resetPassworDto">Password reset data.</param>
        /// <returns>
        /// Returns status code 200 (OK) if the password reset is successful.
        /// Returns status code 400 (Bad Request) if the reset fails.
        /// </returns>
        [HttpPost("reset-password")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ResetPassword(ResetPassworDto resetPassworDto)
        {
            var result = await _service.AuthenticationService.ResetUserPasswordAsync(resetPassworDto);
            if(result.IsSuccess) return Ok(result);
            return BadRequest();
        }
    }
}
