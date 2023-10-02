using Entity.Models;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Service
{
    public interface ICategoryService
    {
        IEnumerable<CategoryDto> GetAllCategories(bool trackChanges);

        CategoryDto GetCategory(Guid categoryId, bool trackChanges);

        CategoryDto CreateCategory(CategoryUpdateCreateDto category);

        void UpdateCategory(Guid categoryId, CategoryUpdateCreateDto categoryUpdate, bool trackChanges);

    }
}
