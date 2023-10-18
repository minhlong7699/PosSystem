using Microsoft.AspNetCore.Identity;
using Shared.DataTransferObjects.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Service
{
    public interface IAuthenticationService
    {
        Task<IdentityResult> RegisterUser(UserForRegistrationDto userForRegistration);
        Task<bool> ValidateUser(UserForAuthenticationDto userForAuth);
        Task<string> CreateToken();
        Task<UserManagerResponse> ForgotPasswordAsync(string email);
        Task<UserManagerResponse> ResetUserPasswordAsync(ResetPassworDto resetPassworDto);

    }
}
