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
    public static class PromotionRepositoryExtension
    {
        // Filtering Promotion
        public static IQueryable<Promotion> FilterPromotion(this IQueryable<Promotion> promotions, DateTime startDate, DateTime endDate)
        {
            return promotions.Where(x => (x.StartDate >= startDate && x.EndDate <= endDate));
        }
        // Searching Promotion
        public static IQueryable<Promotion> SearchPromotion(this IQueryable<Promotion> promotions, string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
                return promotions;

            var lowerCaseSearchTerm = searchTerm.Trim().ToLower();
            return promotions.Where(x => x.PromotionName.ToLower().Contains(lowerCaseSearchTerm));
        }
        // Ordering Promotion

        public static IQueryable<Promotion> SortPromotion(this IQueryable<Promotion> promotions, string orderByQueryString)
        {
            // if Null return product Orderby derfault (orderBy Name)
            if (string.IsNullOrEmpty(orderByQueryString))
                return promotions.OrderBy(x => x.DisCountPercent);

            var orderQuery = OrderQueryBuilder.CreateOrderQuery<Promotion>(orderByQueryString);

            if (string.IsNullOrWhiteSpace(orderQuery))
                return promotions.OrderBy(x => x.DisCountPercent);

            return promotions.OrderBy(orderQuery);
        }
    }
}
