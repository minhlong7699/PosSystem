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
    internal sealed class PromotionService : IPromotionService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public PromotionService(IRepositoryManager repository, ILogger logger, AutoMapper.IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<PromotionDto> CreatepromotionAsync(PromotionUpdateCreateDto promotionCreate)
        {
            var promotionEntity = _mapper.Map<Promotion>(promotionCreate);
            promotionEntity.CreatedAt = DateTime.Now;
            promotionEntity.CreatedBy = "Admin";
            promotionEntity.UpdatedAt = DateTime.Now;
            promotionEntity.UpdatedBy = "Admin";
            _repository.PromotionRepository.CreatePromotion(promotionEntity);
            await _repository.SaveAsync();
            var promotionDto = _mapper.Map<PromotionDto>(promotionEntity);
            return promotionDto;
        }

        public async Task DeletepromotionAsync(Guid promotionId, bool trackChanges)
        {
            var promotionEntity = await _repository.PromotionRepository.GetPromotionAsync(promotionId, trackChanges);
            if (promotionEntity is null) throw new PromotionNotFoundException(promotionId);
            _repository.PromotionRepository.DeletePromotion(promotionEntity);
            await _repository.SaveAsync();
        }

        public async Task<IEnumerable<PromotionDto>> GetAllPromotionsAsync(bool trackChanges)
        {
            var promotions = await _repository.PromotionRepository.GetAllPromotionsAsync(trackChanges);
            var promotionDto = _mapper.Map<IEnumerable<PromotionDto>>(promotions);
            return promotionDto;
        }

        public async Task<PromotionDto> GetPromotionAsync(Guid promotionId, bool trackChanges)
        {
            var promotion = await _repository.PromotionRepository.GetPromotionAsync(promotionId, trackChanges);
            if (promotion is null)
            {
                throw new PromotionNotFoundException(promotionId);
            }
            var promotionDto = _mapper.Map<PromotionDto>(promotion);
            return promotionDto;
        }

        public async Task UpdatePromotionAsync(Guid promotionId, PromotionUpdateCreateDto promotionUpdate, bool trackChanges)
        {
            var promotionEntity = await _repository.PromotionRepository.GetPromotionAsync(promotionId, trackChanges);
            if (promotionEntity is null) throw new PromotionNotFoundException(promotionId);
            promotionEntity.UpdatedAt = DateTime.UtcNow;
            promotionEntity.UpdatedBy = "Admin";
            _mapper.Map(promotionUpdate, promotionEntity);
            await _repository.SaveAsync();
        }
    }
}