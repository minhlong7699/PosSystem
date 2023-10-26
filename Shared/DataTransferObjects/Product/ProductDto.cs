using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects.Product
{
    public record ProductDto(Guid ProductId, string ProductName, string ProductCode, string ProductDescription, decimal ProductPrice, decimal ProductPriceAfterDiscount, int StockQuantity, string Image, bool IsActive, Guid PromotionId, Guid SupplierId);
}