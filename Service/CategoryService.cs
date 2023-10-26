using AutoMapper;
using Contract;
using Contract.Service;
using Contract.Service.UserProvider;
using Entity.Exceptions;
using Entity.Models;
using Serilog;
using Shared.DataTransferObjects.Category;
using Shared.RequestFeatures;

namespace Service
{
    internal sealed class CategoryService : ICategoryService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IUserProvider _userProvider;

        public CategoryService(IRepositoryManager repository, ILogger logger, IMapper mapper, IUserProvider userProvider)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _userProvider = userProvider;
        }


        // Get All Categories
        public async Task<(IEnumerable<CategoryDto> categories , MetaData metaData)> GetAllCategoriesAsync(CategoryParameters categoryParamaters, bool trackChanges)
        {
            var categoryMetadata = await _repository.CategoryRepository.GetAllCategoriesAsync(categoryParamaters,trackChanges);
            var categoriesDto = _mapper.Map<IEnumerable<CategoryDto>>(categoryMetadata);
            return (categories : categoriesDto, metaData: categoryMetadata.MetaData);
        }



        // Get category by Id
        public async Task<CategoryDto> GetCategoryAsync(Guid categoryId, bool trackChanges)
        {
            var category = await _repository.CategoryRepository.GetCategoryAsync(categoryId, trackChanges);
            if (category is null)
            {
                throw new CategoryNotFoundException(categoryId);
            }
            var categoryDto = _mapper.Map<CategoryDto>(category);
            return categoryDto;
        }


        // Create new category
        public async Task<CategoryDto> CreateCategoryAsync(CategoryUpdateCreateDto category)
        {
            var userId = await _userProvider.GetUserIdAsync();
            var categoryEntity = _mapper.Map<Category>(category);
            categoryEntity.CreateAuditFields(userId);
            _repository.CategoryRepository.CreateCategory(categoryEntity);
            await _repository.SaveAsync();

            var CategoryToReturn = _mapper.Map<CategoryDto>(categoryEntity);
            return CategoryToReturn;          
        }


        // Update category
        public async Task UpdateCategoryAsync(Guid categoryId, CategoryUpdateCreateDto categoryUpdate, bool trackChanges)
        {
            var userId = await _userProvider.GetUserIdAsync();
            var categoryEntity = await _repository.CategoryRepository.GetCategoryAsync(categoryId, trackChanges);
            if(categoryEntity is null)
            {
                throw new CategoryNotFoundException(categoryId);
            }
            categoryEntity.UpdateAuditFields(userId);
            _mapper.Map(categoryUpdate, categoryEntity);
            await _repository.SaveAsync();
        }

        public async Task DeleteCategoryAsync(Guid categoryId, bool trackChanges)
        {
            var categoryEntity = await _repository.CategoryRepository.GetCategoryAsync(categoryId, trackChanges);
            if (categoryEntity is null)
            {
                throw new CategoryNotFoundException(categoryId);
            }
            _repository.CategoryRepository.DeleteCategory(categoryEntity);
            await _repository.SaveAsync();
        }
    }
}
