using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects.OrderItem
{
    public record OrderItemsDto(Guid OrderItemsId, int Quantity, decimal Price, Guid ProductId, Guid OrderId);
}
