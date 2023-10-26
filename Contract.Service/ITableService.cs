using Shared.DataTransferObjects.Table;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Service
{
    public interface ITableService
    {
        Task<(IEnumerable<TableDto> tables, MetaData metadata)> GetAllTableAsync(TableParameters tableParameters, bool trackChanges);

        Task<TableDto> GetTableAsync(Guid tableId, bool trackChanges);

        Task<TableDto> CreateTableAsync(TableUpdateCreateDto tableCreate);

        Task UpdateTableAsync(Guid tableId,TableUpdateCreateDto tableUpdate, bool trackChanges);

        Task DeleteTableAsync(Guid tableId, bool trackChanges);
    }
}
