using Contract;
using Entity.Models;
using Microsoft.EntityFrameworkCore;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class OrderItemRepository : RepositoryBase<OrderItem>, IOrderItemRepository
    {
        public OrderItemRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public void CreateOrderItems(Guid orderId, OrderItem orderItem)
        {
            orderItem.OrderId = orderId;
            Create(orderItem);
        }

        public void DeleteOrderItems(OrderItem orderItem)
        {
            Delete(orderItem);
        }

        public async Task<PagedList<OrderItem>> GetAllOrderItemAsync(Guid orderId, OrderItemsParameters orderItemsParameters, bool trackChanges)
        {
            var orderItems = await FindByConditon(x => x.OrderId.Equals(orderId), trackChanges).ToListAsync();

            return PagedList<OrderItem>.ToPagedList(orderItems, orderItemsParameters.pageNumber, orderItemsParameters.pageSize);
        }

        public async Task<OrderItem> GetOrderItemAsync(Guid orderId, Guid orderItemsId, bool trackChanges)
        {
            var orderitem = await FindByConditon(x => x.OrderId.Equals(orderId) && x.OrderItemsId.Equals(orderItemsId), trackChanges).SingleOrDefaultAsync();
            return orderitem;
        }

        public async Task<IEnumerable<OrderItem>> GetOrderItemsForInvoiceAsync(Guid orderId, bool trackChanges)
        {
            var orderItems = await FindByConditon(x => x.OrderId.Equals(orderId), trackChanges).ToListAsync();
            return orderItems;
        }
    }
}
