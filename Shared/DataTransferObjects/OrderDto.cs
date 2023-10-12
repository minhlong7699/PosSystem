using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public record OrderDto(Guid OrderId, string? OrderType, string? Status, Guid UserId, Guid PaymentId, Guid? TableId, Guid TaxId, Guid? PromotionId, DateTime CreatedAt);
}
