using AutoMapper;
using Contract.Service;
using Entity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Serilog;
using Shared.DataTransferObjects.Authentication;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System.Data;
using Entity.Exceptions;
using Contract.Service.EmailServices;
using Entity.Models.Email;
using Microsoft.AspNetCore.WebUtilities;

namespace Service
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;

        private User? _user;
        public AuthenticationService(ILogger logger, IMapper mapper, UserManager<User> userManager, IConfiguration configuration, IEmailService emailService)
        {
            _userManager = userManager;
            _configuration = configuration;
            _logger = logger;
            _mapper = mapper;
            _emailService = emailService;
        }


        public async Task<IdentityResult> RegisterUser(UserForRegistrationDto userForRegistration)
        {
            var user = _mapper.Map<User>(userForRegistration);
            var result = await _userManager.CreateAsync(user, userForRegistration.Password);
            if (result.Succeeded)
                await _userManager.AddToRolesAsync(user, userForRegistration.Roles);
            return result;
        }

        public async Task<bool> ValidateUser(UserForAuthenticationDto userForAuth)
        {

            // Check user IsExisted in Db
            _user = await _userManager.FindByNameAsync(userForAuth.UserName);
            var result = (_user != null && await _userManager.CheckPasswordAsync(_user, userForAuth.Password));
            if (!result)
                _logger.Warning($"{nameof(ValidateUser)}: Authentication failed. Wrong user name or password.");
            return result;
        }

        public async Task<string> CreateToken()
        {
            var signingCredentials = GetSigningCredentials();
            var claims = await GetClaims();
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);
            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }

        private async Task<List<Claim>> GetClaims()
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, _user.UserName)
            };
            var roles = await _userManager.GetRolesAsync(_user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            return claims;
        }

        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var tokenOptions = new JwtSecurityToken
            (
            issuer: jwtSettings["validIssuer"],
            audience: jwtSettings["validAudience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["expires"])),
            signingCredentials: signingCredentials
            );
            return tokenOptions;
        }

        private SigningCredentials GetSigningCredentials()
        {
            var key = Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("SECRET"));
            var secret = new SymmetricSecurityKey(key);
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        public async Task<UserManagerResponse> ForgotPasswordAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user is null) throw new UserNotFoundException(email);
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var encodedToken = Encoding.UTF8.GetBytes(token);
            var validToken = WebEncoders.Base64UrlEncode(encodedToken);
            string url = $"{_configuration["AppUrl"]}/resetpassword?email={email}&token={validToken}";
            Message emailmessage = new Message(new string[] { email }, "ResetPassword", url);
            _emailService.SendEmail(emailmessage);
            return new UserManagerResponse { IsSuccess = true, ErrorMessage = "Reset Password has been sent to your email address" };
        }

        public async Task<UserManagerResponse> ResetUserPasswordAsync(ResetPassworDto resetPassworDto)
        {
            var user = await _userManager.FindByEmailAsync(resetPassworDto.Email);
            if (user is null) throw new UserNotFoundException(resetPassworDto.Email);
            var decodeToken = WebEncoders.Base64UrlDecode(resetPassworDto.Token);
            string rawToken = Encoding.UTF8.GetString(decodeToken);
            var result = await _userManager.ResetPasswordAsync(user, rawToken, resetPassworDto.Password);
            if (result.Succeeded) return new UserManagerResponse { IsSuccess = true, ErrorMessage = "Password has been reset successfully" };
            return new UserManagerResponse { IsSuccess = false, ErrorMessage = "some thing went wrong" };
        }
    }
}
