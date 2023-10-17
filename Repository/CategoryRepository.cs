using Contract;
using Entity.Models;
using Microsoft.EntityFrameworkCore;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }


        public async Task<PagedList<Category>> GetAllCategoriesAsync(CategoryParameters categoryParamaters, bool trackChanges)
        {
            var category = await FindAll(trackChanges)
                .OrderBy(c => c.CategoryName)
                .ToListAsync();

            return PagedList<Category>.ToPagedList(category, categoryParamaters.pageNumber, categoryParamaters.pageSize);
        }

        public async Task<Category> GetCategoryAsync(Guid categoryId, bool trackChanges)
        {
            return await FindByConditon(c => c.CategoryId.Equals(categoryId), trackChanges).SingleOrDefaultAsync();
        }

        public void CreateCategory(Category category)
        {
            Create(category);
        }

        public void DeleteCategory(Category category)
        {
            Delete(category);
        }
    }
}
