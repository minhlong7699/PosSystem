using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects.Authentication
{
    public record ResetPassworDto(string Token, string Email, string Password);
}
