using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Service
{
    public interface IUserRoleService
    {
        Task<IEnumerable<UserRoleDto>> GetAllUserRoleAsync(bool trackChanges);

        Task<UserRoleDto> GetUserRoleAsync(Guid userRoleId, bool trackChanges);
    }
}
