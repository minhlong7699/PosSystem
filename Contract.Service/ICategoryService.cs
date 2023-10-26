using Entity.Models;
using Shared.DataTransferObjects.Category;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Service
{
    public interface ICategoryService
    {
        Task<(IEnumerable<CategoryDto> categories , MetaData metaData)> GetAllCategoriesAsync(CategoryParameters categoryParamaters ,bool trackChanges);

        Task<CategoryDto> GetCategoryAsync(Guid categoryId, bool trackChanges);

        Task<CategoryDto> CreateCategoryAsync(CategoryUpdateCreateDto category);

        Task UpdateCategoryAsync(Guid categoryId, CategoryUpdateCreateDto categoryUpdate, bool trackChanges);

        Task DeleteCategoryAsync(Guid categoryId, bool trackChanges);

    }
}
