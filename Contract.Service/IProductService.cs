using Shared.DataTransferObjects;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Service
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllProductsAsync(Guid categoryId, ProductParameters productParameters, bool trackChanges);

        Task<ProductDto> GetProductAsync(Guid categoryId, Guid productId, bool trackChanges);

        Task<ProductDto> CreateProductAsync(Guid categoryId, ProductUpdateCreateDto productCreate, bool trackChanges);

        Task UpdateProductAsync(Guid categoryId, Guid productId, ProductUpdateCreateDto productUpdate, bool categoryTrachkChanges, bool productTrachChages);
    }
}
