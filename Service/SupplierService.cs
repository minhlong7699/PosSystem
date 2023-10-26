using AutoMapper;
using Contract;
using Contract.Service;
using Contract.Service.UserProvider;
using Entity.Exceptions;
using Entity.Models;
using Serilog;
using Shared.DataTransferObjects.Supplier;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    internal sealed class SupplierService : ISupplierService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IUserProvider _userProvider;
        public SupplierService(IRepositoryManager repository, ILogger logger, AutoMapper.IMapper mapper, IUserProvider userProvider)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _userProvider = userProvider;
        }

        public async Task<SupllierDto> CreateSupplierAsync(SupplierCreateUpdateDto supplierCreate)
        {
            var userId = await _userProvider.GetUserIdAsync();
            var supplierEntity = _mapper.Map<Supplier>(supplierCreate);
            supplierEntity.CreateAuditFields(userId);
            _repository.SupplierRepository.CreateSupplier(supplierEntity);
            var supplierDto = _mapper.Map<SupllierDto>(supplierEntity);
            await _repository.SaveAsync();
            return supplierDto;
        }

        public async Task DeleteSupplierAsync(Guid supplierId, bool trackChanges)
        {
            var supplier = await _repository.SupplierRepository.GetSupplierAsync(supplierId, trackChanges);
            if (supplier is null)
            {
                throw new SupplierNotFoundException(supplierId);
            }
            _repository.SupplierRepository.DeleteSupplier(supplier);
            await _repository.SaveAsync();
        }

        public async Task<(IEnumerable<SupllierDto> suppliers, MetaData metadata)> GetAllSuppliersAsync(SupplierParameter supplierParameter,bool trackChanges)
        {
            var suppilers = await _repository.SupplierRepository.GetAllSuppliersAsync(supplierParameter,trackChanges);
            var supplierDto = _mapper.Map<IEnumerable<SupllierDto>>(suppilers);
            return (suplliers: supplierDto, metadata: suppilers.MetaData);
        }

        public async Task<SupllierDto> GetSupplierAsync(Guid supplierId, bool trackChanges)
        {
            var supplier = await _repository.SupplierRepository.GetSupplierAsync(supplierId, trackChanges);
            var supplerDto = _mapper.Map<SupllierDto>(supplier);
            return supplerDto;
        }

        public async Task UpdateSupplierAsync(Guid supplierId, SupplierCreateUpdateDto supplierUpdate, bool trackChanges)
        {
            var userId = await _userProvider.GetUserIdAsync();
            var supplierEntity = await _repository.SupplierRepository.GetSupplierAsync(supplierId, trackChanges);
            if (supplierEntity is null) throw new SupplierNotFoundException(supplierId);
            supplierEntity.UpdateAuditFields(userId);
            _mapper.Map(supplierUpdate, supplierEntity);
            await _repository.SaveAsync();
        }
    }
}
