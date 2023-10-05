using Entity.Models;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract
{
    public interface IUserRepository
    {
        Task<PagedList<User>> GetAllUsersAsync(UserParamters userParamters,bool trackChanges);
        Task<User> GetUserAsync(Guid userId, bool trackChanges);

        void CreateUser(User user);

        Task<bool> IsUserExistAsync(string userName, bool trackChanges);
    }
}
