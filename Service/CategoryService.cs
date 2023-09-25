using AutoMapper;
using Contract;
using Contract.Service;
using Entity.Exceptions;
using Serilog;
using Shared.DataTransferObjects;

namespace Service
{
    internal sealed class CategoryService : ICategoryService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public CategoryService(IRepositoryManager repository, ILogger logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }


        public IEnumerable<CategoryDto> GetAllCategories(bool trackChanges)
        {

            var categories = _repository.CategoryRepository.GetAllCategories(trackChanges);
            var categoriesDto = _mapper.Map<IEnumerable<CategoryDto>>(categories);
            return categoriesDto;
        }

        public CategoryDto GetCategory(Guid categoryId, bool trackChanges)
        {
            var category = _repository.CategoryRepository.GetCategory(categoryId, trackChanges);
            if(category is null)
            {
                throw new CategoryNotFoundException(categoryId);
            }
            var categoryDto = _mapper.Map<CategoryDto>(category);
            return categoryDto;
        }
    }
}
