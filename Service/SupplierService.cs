using AutoMapper;
using Contract;
using Contract.Service;
using Entity.Exceptions;
using Entity.Models;
using Serilog;
using Shared.DataTransferObjects;
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

        public SupplierService(IRepositoryManager repository, ILogger logger, AutoMapper.IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<SupllierDto> CreateSupplierAsync(SupplierCreateUpdateDto supplierCreate)
        {
            var supplierEntity = _mapper.Map<Supplier>(supplierCreate);
            supplierEntity.CreatedAt = DateTime.Now;
            supplierEntity.CreatedBy = "Admin";
            supplierEntity.UpdatedAt = DateTime.Now;
            supplierEntity.UpdatedBy = "Admin";
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

        public async Task<IEnumerable<SupllierDto>> GetAllSuppliersAsync(bool trackChanges)
        {
            var suppilers = await _repository.SupplierRepository.GetAllSuppliersAsync(trackChanges);
            var supplierDto = _mapper.Map<IEnumerable<SupllierDto>>(suppilers);
            return supplierDto;
        }

        public async Task<SupllierDto> GetSupplierAsync(Guid supplierId, bool trackChanges)
        {
            var supplier = await _repository.SupplierRepository.GetSupplierAsync(supplierId, trackChanges);
            var supplerDto = _mapper.Map<SupllierDto>(supplier);
            return supplerDto;
        }

        public async Task UpdateSupplierAsync(Guid supplierId, SupplierCreateUpdateDto supplierUpdate, bool trackChanges)
        {
            var supplierEntity = await _repository.SupplierRepository.GetSupplierAsync(supplierId, trackChanges);
            if (supplierEntity is null) throw new SupplierNotFoundException(supplierId);
            supplierEntity.UpdatedAt = DateTime.UtcNow;
            supplierEntity.UpdatedBy = "Admin";
            _mapper.Map(supplierUpdate, supplierEntity);
            await _repository.SaveAsync();
        }
    }
}
