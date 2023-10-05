using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Exceptions
{
    public class UserNameConflictException : ConflictExeception
    {
        public UserNameConflictException(string? userName) : base($"your UserName {userName} already existed")
        {
        }
    }
}
