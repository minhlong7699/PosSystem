using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public record UserDto(Guid UserId, string UserName, string FirstName, string LastName, DateTime DateOfBirth, string Address, int PhoneNumber, string EmailAddress, string Image, Guid RoleId);
}
