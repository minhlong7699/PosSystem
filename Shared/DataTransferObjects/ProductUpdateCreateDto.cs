using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public record ProductUpdateCreateDto(string ProductName, string ProductCode, string ProductDescription, string Image, decimal ProductPrice, int StockQuantity, Guid? PromotionId, Guid? SupplierId);
}
