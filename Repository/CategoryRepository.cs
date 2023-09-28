using Contract;
using Entity.Models;
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


        public IEnumerable<Category> GetAllCategories(bool trackChanges)
        {
            return FindAll(trackChanges)
                .OrderBy(c => c.CategoryName)
                .ToList();
        }

        public Category GetCategory(Guid categoryId, bool trackChanges)
        {
            return FindByConditon(c => c.CategoryId.Equals(categoryId), trackChanges).SingleOrDefault();
        }

        public void CreateCategory(Category category)
        {
            Create(category);
        }

    }
}
