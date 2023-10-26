using Entity.Models;
using Repository.Extensions.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
namespace Repository.Extensions
{
    public static class SupplierRepositoryExtension
    {
        // Searching supplier

        public static IQueryable<Supplier> SearchSupplier(this IQueryable<Supplier> suppliers, string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
                return suppliers;
            var lowerCaseSearchTerm = searchTerm.Trim().ToLower();
            return suppliers.Where(x => x.SupplierName.ToLower().Contains(lowerCaseSearchTerm));
        }

        // Sorting supplier
        public static IQueryable<Supplier> SortSupplier(this IQueryable<Supplier> suppliers, string orderByQueryString)
        {
            // if Null return product Orderby derfault (orderBy Name)
            if (string.IsNullOrEmpty(orderByQueryString))
                return suppliers.OrderBy(x => x.SupplierName);

            var orderQuery = OrderQueryBuilder.CreateOrderQuery<Supplier>(orderByQueryString);

            if (string.IsNullOrWhiteSpace(orderQuery))
                return suppliers.OrderBy(x => x.SupplierName);

            return suppliers.OrderBy(orderQuery);
        }

    }
}
