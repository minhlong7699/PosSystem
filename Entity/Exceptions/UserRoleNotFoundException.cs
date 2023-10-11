using Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Exceptions
{
    public class UserRoleNotFoundException : NotFoundException
    {
        public UserRoleNotFoundException(Guid userRoleId) : base($"UserRole with id: {userRoleId} doesn't exist in the database.")
        {
        }
    }
}
