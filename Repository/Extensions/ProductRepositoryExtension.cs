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
    public static class ProductRepositoryExtension
    {
        // Product Filtering
        public static IQueryable<Product> FilterProduct(this IQueryable<Product> products , uint minPrice, uint maxPrice, bool isActive)
        {
            return products.Where(x => (x.ProductPrice >= minPrice && x.ProductPrice <= maxPrice && x.IsActive == isActive));
        }
        // Product Searching

        public static IQueryable<Product> SearchProduct(this IQueryable<Product> products, string searchTerm)
        {
            if(string.IsNullOrEmpty(searchTerm))
                return products;

            var lowerCaseSearchTerm = searchTerm.Trim().ToLower();
            return products.Where(x => x.ProductName.ToLower().Contains(searchTerm));
        }

        // Product Sorting (OrderBy)
        public static IQueryable<Product> SortProduct(this IQueryable<Product> products, string orderByQueryString)
        {
            // if Null return product Orderby derfault (orderBy Name)
            if (string.IsNullOrEmpty(orderByQueryString))
                return products.OrderBy(x => x.ProductName);

            var orderQuery = OrderQueryBuilder.CreateOrderQuery<Product>(orderByQueryString);

            if (string.IsNullOrWhiteSpace(orderQuery))
                return products.OrderBy(x => x.ProductName);

            return products.OrderBy(orderQuery); 
        }


    }
}
