using AutoMapper;
using Contract;
using Contract.Service;
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
    }
}
