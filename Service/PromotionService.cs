using AutoMapper;
using Contract;
using Contract.Service;
using Contract.Service.UserProvider;
using Entity.Exceptions;
using Entity.Models;
using MailKit;
using Org.BouncyCastle.Asn1.Cms;
using Serilog;
using Shared.DataTransferObjects.Promotion;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaData = Shared.RequestFeatures.MetaData;

namespace Service
{
    internal sealed class PromotionService : IPromotionService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IUserProvider _userProvider;

        public PromotionService(IRepositoryManager repository, ILogger logger, AutoMapper.IMapper mapper, IUserProvider userProvider)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _userProvider = userProvider;
        }

        public async Task<PromotionDto> CreatepromotionAsync(PromotionUpdateCreateDto promotionCreate)
        {
            var userId = await _userProvider.GetUserIdAsync();
            var promotionEntity = _mapper.Map<Promotion>(promotionCreate);
            promotionEntity.CreateAuditFields(userId);
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

        public async Task<(IEnumerable<PromotionDto> promotions, MetaData metadata)> GetAllPromotionsAsync(PromotionParamaters promotionParamaters ,bool trackChanges)
        {
            var promotions = await _repository.PromotionRepository.GetAllPromotionsAsync(promotionParamaters ,trackChanges);
            var promotionDto = _mapper.Map<IEnumerable<PromotionDto>>(promotions);
            return (promotions : promotionDto, metadata : promotions.MetaData);
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
            var userId = await _userProvider.GetUserIdAsync();
            var promotionEntity = await _repository.PromotionRepository.GetPromotionAsync(promotionId, trackChanges);
            if (promotionEntity is null) throw new PromotionNotFoundException(promotionId);
            promotionEntity.UpdateAuditFields(userId);
            _mapper.Map(promotionUpdate, promotionEntity);
            await _repository.SaveAsync();
        }

    }
}