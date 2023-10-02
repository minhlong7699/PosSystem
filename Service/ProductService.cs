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
        private readonly IUploadImageService _uploadImageService;

        public ProductService(IRepositoryManager repository, ILogger logger, AutoMapper.IMapper mapper , IUploadImageService uploadImageService)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _uploadImageService = uploadImageService;
        }

        // Create
        public ProductDto CreateProduct(Guid categoryId, ProductUpdateCreateDto productCreate, bool trackChanges)
        {
            // Check category is exist
            var category = _repository.CategoryRepository.GetCategory(categoryId, trackChanges);
            if (category is null)
            {
                throw new CategoryNotFoundException(categoryId);
            }
            // check suppiler is exist
            var supplier = _repository.SupplierRepository.GetSupplier(productCreate.SupplierId, trackChanges);
            if (supplier is null)
            {
                throw new SupplierNotFoundException(productCreate.SupplierId);
            }
            // mapping from Dto to Product Entity
            var productEntity = _mapper.Map<Product>(productCreate);
            // Save Img to Server and get String to return
            productEntity.Image = _uploadImageService.GetImageUrl(productCreate.Image, "ProductImages");
            // get value of Promotionid from Dto if existed
            Guid actualPromotionId = productCreate.PromotionId ?? Guid.Empty;
            // Check promotion of product is exist
            var promotionOfProduct = _repository.PromotionRepository.GetPromotion(actualPromotionId, trackChanges: false);
            if(promotionOfProduct is null)
            {
                productEntity.ProductPriceAfterDiscount = productEntity.ProductPrice;
            }
            else
            {
                productEntity.ProductPriceAfterDiscount = productEntity.ProductPrice * (decimal)promotionOfProduct.DisCountPercent / 100;
            }

            // Add value to some private field
            productEntity.CreatedAt = DateTime.Now;
            productEntity.CreatedBy = "Admin";
            productEntity.UpdatedAt = DateTime.Now;
            productEntity.UpdatedBy = "Admin";
            _repository.ProductRepository.CreateProduct(categoryId, productEntity);
            _repository.Save();

            var ProductToReturn = _mapper.Map<ProductDto>(productEntity);

            return ProductToReturn;
        }

        // Get All 
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

        // Get By Id
        public ProductDto GetProduct(Guid categoryId, Guid productId, bool trackChanges)
        {
            var category = _repository.CategoryRepository.GetCategory(categoryId, trackChanges);
            if (category is null)
            {
                throw new CategoryNotFoundException(categoryId);
            }
            var product = _repository.ProductRepository.GetProduct(categoryId, productId, trackChanges);
            if (product is null)
            {
                throw new ProductNotFoundException(productId);
            }
            var productDto = _mapper.Map<ProductDto>(product);
            return productDto;
        }

        public void UpdateProduct(Guid categoryId, Guid productId, ProductUpdateCreateDto productUpdate, bool categoryTrachkChanges, bool productTrachChages)
        {
            var category = _repository.CategoryRepository.GetCategory(categoryId, categoryTrachkChanges);
            if (category is null)
            {
                throw new CategoryNotFoundException(categoryId);
            }
            var productEntity = _repository.ProductRepository.GetProduct(categoryId, productId, productTrachChages);

            var image = productEntity.Image;

            if (productEntity is null)
            {
                throw new ProductNotFoundException(productId);
            }
            // Set the values for private fields
            if (productUpdate.Image is not null)
            {
                productEntity.Image = _uploadImageService.GetImageUrl(productUpdate.Image, "ProductImages");
            }
            else
            {
                productEntity.Image = image;
            }    
            productEntity.UpdatedAt = DateTime.Now;
            productEntity.UpdatedBy = "Admin";
            _mapper.Map(productUpdate, productEntity);
            _repository.Save();
        }
    }
}
