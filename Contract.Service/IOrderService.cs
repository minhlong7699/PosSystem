using Shared.DataTransferObjects.Order;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Service
{
    public interface IOrderService
    {
        Task<(IEnumerable<OrderDto> orders, MetaData metaData)> GetAllOrderAsync(OrderParameters orderParameters, bool trackChanges);

        Task<OrderDto> GetOrderAsync(Guid orderId, bool trackChanges);

        Task<OrderDto> CreateOrderAsync(OrderCreateUpdateDto orderCreate, bool trackChanges);

        Task UpdateOrderAsync(Guid orderId, OrderCreateUpdateDto orderUpdate, bool trackChanges);

        Task DeleteOrderAsync(Guid orderId, bool trackChanges);
    }
}
