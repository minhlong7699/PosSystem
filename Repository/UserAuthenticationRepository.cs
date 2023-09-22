using Contract;
using Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class UserAuthenticationRepository : RepositoryBase<UserAuthentication>, IUserAuthenticationRepository
    {
        public UserAuthenticationRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }
    }
}
