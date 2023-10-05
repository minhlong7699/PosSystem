using Shared.DataTransferObjects;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Service
{
    public interface IUserService
    {
        Task<(IEnumerable<UserDto> users, MetaData metaData)> GetAllUsersAsync(UserParamters userParamters, bool trackChanges);

        Task<UserDto> GetUserAsync(Guid userId, bool trackChanges);

        Task<UserDto> CreateUserAsync(UserUpdateCreateDto userCreate, bool trackChanges);
    }
}
