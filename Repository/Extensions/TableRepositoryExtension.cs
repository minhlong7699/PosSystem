using Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Extensions
{
    public static class TableRepositoryExtension
    {
        // Filtering Table

        public static IQueryable<Table> FilterTable(this IQueryable<Table> tables, bool IsOccupied, bool IsActive)
        {
            return tables.Where(t => t.IsOccupied == IsOccupied && t.IsActive == IsActive);
        } 

        // Searching Table

        public static IQueryable<Table> SearchTable(this IQueryable<Table> tables, string searchTerm)
        {
            if(searchTerm is null) return tables;
            var lowerCaseSearchTerm = searchTerm.Trim().ToLower();
            return tables.Where(t => t.Name.ToLower().Contains(lowerCaseSearchTerm));
        }
    }
}
