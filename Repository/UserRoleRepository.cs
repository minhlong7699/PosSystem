using Contract;
using Entity.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class UserRoleRepository : RepositoryBase<UserRole>, IUserRoleRepository
    {
        public UserRoleRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<UserRole>> GetAllUserRolesAsync(bool trackChanges)
        {
            var roles = await FindAll(trackChanges).ToListAsync();
            return roles;
        }

        public async Task<UserRole> GetUserRoleAsync(Guid userRoleId, bool trackChanges)
        {
            var role = await FindByConditon(x => x.RoleId.Equals(userRoleId), trackChanges).SingleOrDefaultAsync();
            return role;
        }
    }
}
