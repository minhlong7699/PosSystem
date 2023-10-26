using AutoMapper;
using Contract;
using Contract.Service;
using Contract.Service.UserProvider;
using Entity.Exceptions;
using Entity.Models;
using Serilog;
using Shared.DataTransferObjects.Product;
using Shared.RequestFeatures;

namespace Service
{
    internal sealed class ProductService : IProductService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IUploadImageService _uploadImageService;
        private readonly IUserProvider _userProvider;

        public ProductService(IRepositoryManager repository, ILogger logger, AutoMapper.IMapper mapper, IUploadImageService uploadImageService, IUserProvider userProvider)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _uploadImageService = uploadImageService;
            _userProvider = userProvider;
        }

        // Create
        public async Task<ProductDto> CreateProductAsync(Guid categoryId, ProductUpdateCreateDto productCreate, bool trackChanges)
        {

            var userId = await _userProvider.GetUserIdAsync();
            await ValidCategoryExist(categoryId, trackChanges);
            await ValidSupplierExist(productCreate.SupplierId, trackChanges);
            var productEntity = _mapper.Map<Product>(productCreate);
            productEntity.Image = _uploadImageService.GetImageUrl(productCreate.Image, "ProductImages");
            Guid actualPromotionId = productCreate.PromotionId ?? Guid.Empty;
            var promotionOfProduct = await _repository.PromotionRepository.GetPromotionAsync(actualPromotionId, trackChanges: false);
            if (promotionOfProduct is null)
            {
                productEntity.ProductPriceAfterDiscount = productEntity.ProductPrice;
            }
            else
            {
                productEntity.ProductPriceAfterDiscount = productEntity.ProductPrice * (decimal)promotionOfProduct.DisCountPercent / 100;
            }
            productEntity.CreateAuditFields(userId);
            _repository.ProductRepository.CreateProduct(categoryId, productEntity);
            await _repository.SaveAsync();
            var ProductToReturn = _mapper.Map<ProductDto>(productEntity);
            return ProductToReturn;
        }

        public async Task DeleteProductAsync(Guid categoryId, Guid productId, bool trackChanges)
        {
            await ValidCategoryExist(categoryId, trackChanges);
            var product = await ValidProductExist(categoryId, productId, trackChanges);
            _repository.ProductRepository.DeleteProduct(product);
            await _repository.SaveAsync();
        }

        // Get All 
        public async Task<(IEnumerable<ProductDto> products, MetaData metaData)> GetAllProductsAsync(Guid categoryId, ProductParameters productParameters, bool trackChanges)
        {
            await ValidCategoryExist(categoryId, trackChanges);      
            var productMetadata = await _repository.ProductRepository.GetAllProductsAsync(categoryId, productParameters, trackChanges);
            var productsDto = _mapper.Map<IEnumerable<ProductDto>>(productMetadata);
            return (products: productsDto, metaData: productMetadata.MetaData);
        }

        // Get By Id
        public async Task<ProductDto> GetProductAsync(Guid categoryId, Guid productId, bool trackChanges)
        {
            await ValidCategoryExist(categoryId, trackChanges);
            var product = await ValidProductExist(categoryId, productId, trackChanges);
            var productDto = _mapper.Map<ProductDto>(product);
            return productDto;
        }

        // Update using Patch
        public async Task<(ProductUpdateCreateDto productToPatch, Product productEntity)> GetProductForPatchAsync(Guid categoryId, Guid productId, bool categoryTrackChanges, bool productTrackChages)
        {
            await ValidCategoryExist(categoryId, categoryTrackChanges);
            var product = await ValidProductExist(categoryId, productId, productTrackChages);
            var productToPatch = _mapper.Map<ProductUpdateCreateDto>(product);
            return (productToPatch, product);

        }

        public async Task SaveChangesForPatchAsync(ProductUpdateCreateDto productToPatch, Product productEntity)
        {
            _mapper.Map(productToPatch, productEntity);
            await _repository.SaveAsync();
        }

        // Update Product
        public async Task UpdateProductAsync(Guid categoryId, Guid productId, ProductUpdateCreateDto productUpdate, bool categoryTrackChanges, bool productTrackChages)
        {
            var userId = await _userProvider.GetUserIdAsync();
            await ValidCategoryExist(categoryId, categoryTrackChanges);
            var productEntity = await _repository.ProductRepository.GetProductAsync(categoryId, productId, productTrackChages);
            productEntity.UpdateAuditFields(userId);
            _mapper.Map(productUpdate, productEntity);
            var image = productEntity.Image;
            if (productEntity is null)
            {
                throw new ProductNotFoundException(productId);
            }
            if (productUpdate.Image is not null)
            {
                productEntity.Image = _uploadImageService.GetImageUrl(productUpdate.Image, "ProductImages");
            }
            else
            {
                productEntity.Image = image;
            }
            await _repository.SaveAsync();
        }

        private async Task ValidCategoryExist(Guid categoryId, bool trackChanges )
        {
            var category = await _repository.CategoryRepository.GetCategoryAsync(categoryId, trackChanges);
            if (category is null)
            {
                throw new CategoryNotFoundException(categoryId);
            }
        }

        private async Task ValidSupplierExist(Guid? supplierId, bool trackChanges)
        {
            var supplier = await _repository.SupplierRepository.GetSupplierAsync(supplierId, trackChanges);
            if (supplier is null)
            {
                throw new SupplierNotFoundException(supplierId);
            }
        }

        private async Task<Product> ValidProductExist(Guid categoryId, Guid productId, bool trackChanges)
        {
            var product = await _repository.ProductRepository.GetProductAsync(categoryId, productId, trackChanges);
            if (product is null)
            {
                throw new ProductNotFoundException(productId);
            }
            return product;
        }
    }
}
