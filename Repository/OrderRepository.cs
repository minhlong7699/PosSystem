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
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        // Create Order
        public void CreateOrder(Order order)
        {
            Create(order);
        }

        // Get All Order
        public async Task<PagedList<Order>> GetAllOrderAsync(OrderParameters orderParameters, bool trackChanges)
        {
            var order = await FindAll(trackChanges).ToListAsync();
            return PagedList<Order>.ToPagedList(order, orderParameters.pageNumber, orderParameters.pageSize);
        } 

        // Get Order By Id
        public async Task<Order> GetOrderAsync(Guid orderId, bool trackChanges)
        {
            var order = await FindByConditon(x => x.OrderId.Equals(orderId), trackChanges).Include(o => o.OrderItems).SingleOrDefaultAsync();
            return order;
        }
    }
}
