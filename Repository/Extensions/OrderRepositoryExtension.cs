using Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Repository.Extensions.Utility;
namespace Repository.Extensions
{
    public static class OrderRepositoryExtension
    {
        public static IQueryable<Order> FilterOrder(this IQueryable<Order> orders, string orderType, string status, bool isActive, string tableId)
        {
            var filteredOrders = orders;
            if (tableId != null)
            {
                filteredOrders = filteredOrders.Where(x => x.TableId.Equals(tableId));
            }
            if (orderType != null)
            {
                filteredOrders = filteredOrders.Where(x => orderType.Contains(x.OrderType));
            }

            if (status != null)
            {
                filteredOrders = filteredOrders.Where(x => status.Contains(x.Status));
            }
            filteredOrders = filteredOrders.Where(x => x.IsActive.Equals(isActive));


            return filteredOrders;
        }


        public static IQueryable<Order> SortOrder(this IQueryable<Order> orders, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
            {
                return orders; // Trả về dữ liệu không được sắp xếp nếu `orderByQueryString` trống.
            }

            var orderQuery = OrderQueryBuilder.CreateOrderQuery<Order>(orderByQueryString);

            if (string.IsNullOrWhiteSpace(orderQuery))
                return orders;

            return orders.OrderBy(orderQuery);
        }


    }
}
