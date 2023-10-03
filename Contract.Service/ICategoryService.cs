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
        Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync(bool trackChanges);

        Task<CategoryDto> GetCategoryAsync(Guid categoryId, bool trackChanges);

        Task<CategoryDto> CreateCategoryAsync(CategoryUpdateCreateDto category);

        Task UpdateCategoryAsync(Guid categoryId, CategoryUpdateCreateDto categoryUpdate, bool trackChanges);

    }
}
