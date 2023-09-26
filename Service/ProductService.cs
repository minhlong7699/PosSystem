using AutoMapper;
using Contract;
using Contract.Service;
using Entity.Exceptions;
using Entity.Models;
using Serilog;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    internal sealed class ProductService : IProductService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public ProductService(IRepositoryManager repository, ILogger logger, AutoMapper.IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public IEnumerable<ProductDto> GetAllProducts(Guid categoryId, bool trackChanges)
        {
            var category = _repository.CategoryRepository.GetCategory(categoryId, trackChanges);
            if (category is null)
            {
                throw new CategoryNotFoundException(categoryId);
            }

            var products = _repository.ProductRepository.GetAllProducts(categoryId, trackChanges);
            var productsDto = _mapper.Map<IEnumerable<ProductDto>>(products);
            return productsDto;
        }
    }
}
