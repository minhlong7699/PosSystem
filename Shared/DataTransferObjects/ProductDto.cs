using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public record ProductDto(Guid ProductId, string ProductName, string ProductCode, string ProductDescription, decimal ProductPrice, decimal ProductPriceAfterDiscount,int StockQuantity, string Image, Guid PromotionId, Guid SupplierId);
}