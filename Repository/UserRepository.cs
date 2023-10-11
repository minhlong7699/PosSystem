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

        public async Task<bool> CheckPasswordAsync( Guid userId ,string? password)
        {
            var user = await FindByConditon(x => x.UserId.Equals(userId) && x.UserPassword.Equals(password), false).SingleOrDefaultAsync();
            return user is null ? false : true;
        }

        public void CreateUser(User user)
        {
            Create(user);
        }

        public async Task<PagedList<User>> GetAllUsersAsync(UserParameters userParamters, bool trackChanges)
        {
            var users = await FindAll(trackChanges).ToListAsync();

            return PagedList<User>.ToPagedList(users, userParamters.pageNumber, userParamters.pageSize);
        }


        public async Task<User> GetUserAsync(Guid userId, bool trackChanges)
        {
            var user = await FindByConditon(x => x.UserId.Equals(userId), trackChanges).SingleOrDefaultAsync();  
            return user;
        }

        public async Task<User> GetUserByName(string userName)
        {
            var user = await FindByConditon(x => x.UserName.Equals(userName), false).SingleOrDefaultAsync();
            if(user is null) return null;
            return user;
        }
    }
}
