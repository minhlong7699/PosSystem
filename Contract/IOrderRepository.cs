using Entity.Models;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract
{
    public interface IOrderRepository
    {
        Task<PagedList<Order>> GetAllOrderAsync(OrderParameters orderParameters, bool trackChanges);

        Task<Order> GetOrderAsync(Guid orderId, bool trackChanges);

        void CreateOrder(Order order);

        void DeleteOrder(Order order);

    }
}
