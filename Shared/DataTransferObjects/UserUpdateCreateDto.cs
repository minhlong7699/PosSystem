using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public record UserUpdateCreateDto(string UserName, string UserPassword, string FirstName, string LastName, DateTime DateOfBirth, int PhoneNumber, IFormFile Image,  string EmailAddress, string Address, Guid RoleId, bool Isactive);
}
