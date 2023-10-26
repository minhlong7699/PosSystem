using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects.OrderItem
{
    public record OrderItemCreateUpdateDto(Guid ProductId, int Quantity, Guid categoryId);
    public record OrderItemUpdateDto(int Quantity);
}
