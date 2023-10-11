using Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract
{
    public interface IUserRoleRepository
    {
        Task<IEnumerable<UserRole>> GetAllUserRolesAsync(bool trackChanges);

        Task<UserRole> GetUserRoleAsync(Guid userRoleId, bool trackChanges);
    }
}
