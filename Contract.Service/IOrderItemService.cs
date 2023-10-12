using Shared.DataTransferObjects;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Service
{
    public interface IOrderItemService
    {
        Task<(IEnumerable<OrderItemsDto> orderItems, MetaData metaData)> GetAllOrderItemsAsync(Guid orderId, OrderItemsParameters orderItemsParameters, bool trackChanges);

        Task<OrderItemsDto> GetOrderItemAsync(Guid orderId, Guid orderItemId, bool trackChanges);

        Task<OrderItemsDto> CreateOrderItemAsync(Guid orderId, OrderItemCreateUpdateDto orderItemCreate, bool trackChanges );

        Task UpdateOrderItemAsync(Guid orderId, Guid orderItemId, OrderItemUpdateDto orderItemUpdate, bool trackChanges);
    }
}
