using AutoMapper;
using Contract;
using Contract.Service;
using Entity.Exceptions;
using Entity.Models;
using Serilog;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Service
{
    internal sealed class TableService : ITableService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public TableService(IRepositoryManager repository, ILogger logger, AutoMapper.IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<TableDto> CreateTableAsync(TableUpdateCreateDto tableCreate)
        {
            var tableEntity = _mapper.Map<Table>(tableCreate);
            tableEntity.CreatedAt = DateTime.Now;
            tableEntity.CreatedBy = "Admin";
            tableEntity.UpdatedAt = DateTime.Now;
            tableEntity.UpdatedBy = "Admin";
            tableEntity.IsOccupied = false;
            _repository.TablesRepository.CreateTable(tableEntity);
            await _repository.SaveAsync();

            var tableDto = _mapper.Map<TableDto>(tableEntity);
            return tableDto;
        }

        public async Task DeleteTableAsync(Guid tableId, bool trackChanges)
        {
            var tableEntity = await _repository.TablesRepository.GetTableAsync(tableId, trackChanges);
            if (tableEntity is null) throw new TableNotFoundException(tableId);
            _repository.TablesRepository.DelteTable(tableEntity);
            await _repository.SaveAsync();
        }

        public async Task<(IEnumerable<TableDto> tables, MetaData metadata)> GetAllTableAsync(TableParameters tableParameters, bool trackChanges)
        {
            var tablesMetadata = await _repository.TablesRepository.GetAllTablesAsync(tableParameters, trackChanges);
            var tableDto = _mapper.Map<IEnumerable<TableDto>>(tablesMetadata);
            return (tableDto, tablesMetadata.MetaData);
        }

        public async Task<TableDto> GetTableAsync(Guid tableId, bool trackChanges)
        {
            var tableEntity = await _repository.TablesRepository.GetTableAsync(tableId, trackChanges);
            if (tableEntity is null)
            {
                throw new TableNotFoundException(tableId);
            }

            var tableDto = _mapper.Map<TableDto>(tableEntity);

            return (tableDto);
        }

        public async Task UpdateTableAsync(Guid tableId,TableUpdateCreateDto tableUpdate, bool trackChanges)
        {
            var tableEntity = await _repository.TablesRepository.GetTableAsync(tableId, trackChanges);
            if (tableEntity is null) throw new TableNotFoundException(tableId);
            tableEntity.UpdatedAt = DateTime.UtcNow;
            tableEntity.UpdatedBy = "Admin";
            _mapper.Map(tableUpdate, tableEntity);
            await _repository.SaveAsync();
        }
    }
}
