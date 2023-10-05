using Contract;
using Entity.Models;
using Microsoft.EntityFrameworkCore;
using Shared.RequestFeatures;

namespace Repository
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public void CreateUser(User user)
        {
            Create(user);
        }

        public async Task<PagedList<User>> GetAllUsersAsync(UserParamters userParamters, bool trackChanges)
        {
            var users = await FindAll(trackChanges).ToListAsync();

            return PagedList<User>.ToPagedList(users, userParamters.pageNumber, userParamters.pageSize);
        }


        public async Task<User> GetUserAsync(Guid userId, bool trackChanges)
        {
            var user = await FindByConditon(x => x.UserId.Equals(userId), trackChanges).SingleOrDefaultAsync();  
            return user;
        }

        public async Task<bool> IsUserExistAsync(string userName, bool trackChanges)
        {
            var user = await FindByConditon(x => x.UserName.Equals(userName), trackChanges).SingleOrDefaultAsync();
            if(user is null)
                return false;
            return true;
        }
    }
}
