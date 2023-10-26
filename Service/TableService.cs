using AutoMapper;
using Contract;
using Contract.Service;
using Contract.Service.UserProvider;
using Entity.Exceptions;
using Entity.Models;
using Serilog;
using Shared.DataTransferObjects.Table;
using Shared.RequestFeatures;

namespace Service
{
    internal sealed class TableService : ITableService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IUserProvider _userProvider;
        public TableService(IRepositoryManager repository, ILogger logger, AutoMapper.IMapper mapper, IUserProvider userProvider)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _userProvider = userProvider;
        }

        public async Task<TableDto> CreateTableAsync(TableUpdateCreateDto tableCreate)
        {
            var userId = await _userProvider.GetUserIdAsync();
            var tableEntity = _mapper.Map<Table>(tableCreate);
            tableEntity.IsOccupied = false;
            tableEntity.CreateAuditFields(userId);
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
            var userId = await _userProvider.GetUserIdAsync();
            var tableEntity = await _repository.TablesRepository.GetTableAsync(tableId, trackChanges);
            if (tableEntity is null) throw new TableNotFoundException(tableId);
            tableEntity.UpdateAuditFields(userId);
            _mapper.Map(tableUpdate, tableEntity);
            await _repository.SaveAsync();
        }
    }
}
