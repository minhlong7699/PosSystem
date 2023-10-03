using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public record ProductUpdateCreateDto(
        string ProductName,
        string ProductCode,
        string ProductDescription,
        IFormFile Image,
        [Range(0.01, double.MaxValue, ErrorMessage = "ProductPrice must be a positive number.")]
        decimal ProductPrice,
        bool IsActive,
        int StockQuantity,
        Guid? PromotionId,
        Guid? SupplierId);
}
