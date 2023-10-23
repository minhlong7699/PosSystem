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
    public class TableRepository : RepositoryBase<Table>, ITableRepository
    {
        public TableRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public void CreateTable(Table table)
        {
            Create(table);
        }

        public void DelteTable(Table table)
        {
            Delete(table);
        }

        public async Task<PagedList<Table>> GetAllTablesAsync(TableParameters tableParameters, bool trackChanges)
        {
            var table = await FindAll(trackChanges: false).ToListAsync();
            return PagedList<Table>.ToPagedList(table, tableParameters.pageNumber, tableParameters.pageSize);
        }

        public async Task<Table> GetTableAsync(Guid tableId, bool trackChanges)
        {
            return await FindByConditon(x => x.TableID.Equals(tableId), trackChanges).SingleOrDefaultAsync();
        }
    }
}
