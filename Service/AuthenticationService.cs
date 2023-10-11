using AutoMapper;
using Contract;
using Contract.Service;
using Entity.Models;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Shared.DataTransferObjects.Authentication;
using System.Xml.Linq;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace Service
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IRepositoryManager _repository;
        private readonly IConfiguration _configuration;
        private User _user;

        public AuthenticationService(IRepositoryManager repository ,ILogger logger, IMapper mapper, IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<bool> ValidateUser(UserForAuthenticationDto userForAuth)
        {

            // Check user IsExisted in Db
            _user = await _repository.UserRepository.GetUserByName(userForAuth.UserName);
            var result = (_user != null && await _repository.UserRepository.CheckPasswordAsync(_user.UserId, userForAuth.UserPassword));
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
            var rolesEntity = await _repository.UserRoleRepository.GetUserRoleAsync(_user.RoleId, trackChanges: false);
            claims.Add(new Claim(ClaimTypes.Role, rolesEntity.RoleName));
            
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


    }
}
