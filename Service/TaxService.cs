using AutoMapper;
using Contract;
using Contract.Service;
using Entity.Exceptions;
using Entity.Models;
using Serilog;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    internal class TaxService : ITaxService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public TaxService(IRepositoryManager repository, ILogger logger, AutoMapper.IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<TaxDto> CreateTax(TaxCreateUpdateDto taxCreate)
        {
            var taxEntity = _mapper.Map<Tax>(taxCreate);
            taxEntity.CreatedAt = DateTime.Now;
            taxEntity.CreatedBy = "Admin";
            taxEntity.UpdatedAt = DateTime.Now;
            taxEntity.UpdatedBy = "Admin";
            _repository.TaxRepository.CreateTax(taxEntity);
            await _repository.SaveAsync();
            var taxDto = _mapper.Map<TaxDto>(taxEntity);
            return taxDto;
        }

        public async Task DeleteTaxAsync(Guid taxId, bool trackChanges)
        {
            var TaxEntity = await _repository.TaxRepository.GetTaxAsync(taxId, trackChanges);
            if (TaxEntity is null) throw new TaxNotFoundExeception(taxId);
            _repository.TaxRepository.DeleteTax(TaxEntity);
            await _repository.SaveAsync();
        }

        public async Task<(IEnumerable<TaxDto> taxes, MetaData metadata)> GetAllTaxesAsync(TaxParameters taxParameters, bool trackChanges)
        {
            var taxMetadata = await _repository.TaxRepository.GetAllTaxesAsync(taxParameters, trackChanges);
            var taxDto = _mapper.Map<IEnumerable<TaxDto>>(taxMetadata);
            return (taxDto, taxMetadata.MetaData);
        }

        public async Task<TaxDto> GetTaxAsync(Guid taxId, bool trackChanges)
        {
            var tax = await _repository.TaxRepository.GetTaxAsync(taxId, trackChanges);
            if (tax is null)
            {
                throw new TaxNotFoundExeception(taxId);
            }
            var taxDto = _mapper.Map<TaxDto>(tax);
            return taxDto;
        }

        public async Task UpdateTaxAsync(Guid taxId, TaxCreateUpdateDto taxUpdate, bool trackChanges)
        {
            var TaxEntity = await _repository.TaxRepository.GetTaxAsync(taxId, trackChanges);
            if (TaxEntity is null) throw new TaxNotFoundExeception(taxId);
            TaxEntity.UpdatedAt = DateTime.UtcNow;
            TaxEntity.UpdatedBy = "Admin";
            _mapper.Map(taxUpdate, TaxEntity);
            await _repository.SaveAsync();
        }
    }
}
