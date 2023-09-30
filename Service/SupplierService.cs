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

        public IEnumerable<SupllierDto> GetAllSuppliers(bool trackChanges)
        {
            var suppilers = _repository.SupplierRepository.GetAllSuppliers(trackChanges);
            var supplierDto = _mapper.Map<IEnumerable<SupllierDto>>(suppilers);
            return supplierDto;
        }

        public SupllierDto GetSupplier(Guid supplierId, bool trackChanges)
        {
            var supplier = _repository.SupplierRepository.GetSupplier(supplierId, trackChanges);
            var supplerDto = _mapper.Map<SupllierDto>(supplier);
            return supplerDto;
        }
    }
}
