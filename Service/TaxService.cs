using AutoMapper;
using Contract;
using Contract.Service;
using Contract.Service.UserProvider;
using Entity.Exceptions;
using Entity.Models;
using Serilog;
using Shared.DataTransferObjects.Tax;
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
        private readonly IUserProvider _userProvider;
        public TaxService(IRepositoryManager repository, ILogger logger, AutoMapper.IMapper mapper, IUserProvider userProvider)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _userProvider = userProvider;
        }

        public async Task<TaxDto> CreateTax(TaxCreateUpdateDto taxCreate)
        {
            var userId = await _userProvider.GetUserIdAsync();
            var taxEntity = _mapper.Map<Tax>(taxCreate);
            taxEntity.CreateAuditFields(userId);
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
            var userId = await _userProvider.GetUserIdAsync();
            var TaxEntity = await _repository.TaxRepository.GetTaxAsync(taxId, trackChanges);
            if (TaxEntity is null) throw new TaxNotFoundExeception(taxId);
            TaxEntity.UpdateAuditFields(userId);
            _mapper.Map(taxUpdate, TaxEntity);
            await _repository.SaveAsync();
        }
    }
}
