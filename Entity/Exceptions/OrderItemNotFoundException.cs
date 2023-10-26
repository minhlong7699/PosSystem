using Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Exceptions
{
    public class OrderItemNotFoundException : NotFoundException
    {
        public OrderItemNotFoundException(Guid orderItemsId) : base($"The orderItem with id: {orderItemsId} doesn't exist in the database.")
        {
        }
    }
}
